﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using DanceBotShared.Common;

namespace TelegramClient.Handlers
{
	public interface IRegexHandler
	{
		BusinessContext GetContextFromMessage(string message);
		string GetMessageFromContext(BusinessContext context);
    }

    internal class RegexHandler : IRegexHandler
    {
		private const string StepSeparator = "%";
		private const string IdSeparator = "#";
		private const string TypeSeparator = "$";

        private const string Regex = "(?<=\\{0}).+?(?=\\{0})";

        public string GetMessageFromContext(BusinessContext context)
        {
            var sb = new StringBuilder();

            sb.Append($"{StepSeparator}{context.Step}{StepSeparator}");

            if (!string.IsNullOrEmpty(context.EntityId))
            {
                sb.Append($"{IdSeparator}{context.EntityId}{IdSeparator}");
            }

            return sb.ToString();
        }

        public BusinessContext GetContextFromMessage(string message)
        {
            var stepRegex = new Regex(string.Format(Regex, StepSeparator));
            var step = stepRegex.Match(message);

            var entityIdRegex = new Regex(string.Format(Regex, IdSeparator));
            var entityId = entityIdRegex.Match(message);

            return new BusinessContext
            {
                Step = step.Success ? step.Value : null,
                EntityId = entityId.Success ? entityId.Value : null
            };
        }
    }
}

