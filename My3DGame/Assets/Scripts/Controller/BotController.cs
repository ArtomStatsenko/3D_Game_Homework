﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace StatsenkoAA
{
    public sealed class BotController : BaseController, IExecute, IInitialization
    {
        private readonly int _countBot = 5;
        private readonly HashSet<Bot> _getBotList = new HashSet<Bot>();

        public void Initialization()
        {
            for (var index = 0; index < _countBot; index++)
            {
                var tempBot = Object.Instantiate(ServiceLocatorMonoBehaviour.GetService<Reference>().Bot,
                    Patrol.GenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform),
                    Quaternion.identity);

                tempBot.Agent.avoidancePriority = index;
                tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
                AddBotToList(tempBot);
            }
        }

        private void AddBotToList(Bot bot)
        {
            if (!_getBotList.Contains(bot))
            {
                _getBotList.Add(bot);
                bot.OnDieChange += RemoveBotToList;
            }
        }

        private void RemoveBotToList(Bot bot)
        {
            if (!_getBotList.Contains(bot))
            {
                return;
            }

            bot.OnDieChange -= RemoveBotToList;
            _getBotList.Remove(bot);
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            for (var i = 0; i < _getBotList.Count; i++)
            {
                var bot = _getBotList.ElementAt(i);
                bot.Tick();
            }
        }
    }
}
