﻿using System.Collections.Generic;
using System.Linq;
using VRC.SDK3.Avatars.Components;
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;

namespace VRCAvatars3Validator
{
    public sealed class VRCAvatars3Validator
    {
        public static Dictionary<int, IEnumerable<ValidateResult>> ValidateAvatars3(VRCAvatarDescriptor avatar, IEnumerable<RuleItem> rules)
        {
            return rules.Select((rule, index) => new { Rule = rule, Index = index})
                .Where(rulePair => rulePair.Rule.Enabled)
                .Select(rulePair =>
                {
                    var rule = RuleUtility.FilePath2IRule(rulePair.Rule.FilePath);
                    var results = rule.Validate(avatar);
                    return new KeyValuePair<int, IEnumerable<ValidateResult>>(rulePair.Index + 1, results);
                })
                .ToDictionary(resultPair => resultPair.Key, resultPair => resultPair.Value);
        }
    }
}