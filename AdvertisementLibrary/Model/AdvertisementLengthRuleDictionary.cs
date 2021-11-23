using AdvertisementLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementLibrary.Model
{
    /// <summary>
    /// Advertisement Length Rule Dictionary
    /// </summary>
    public class AdvertisementLengthRuleDictionary
    {
        /// <summary>
        /// Advertisement Length Rule Readonly Dictionary
        /// </summary>
        private IReadOnlyDictionary<(int MinLength, int MaxLength), decimal> RuleDictionary { get; } = new Dictionary<(int MinLength, int MaxLength), decimal>()
        {
            { (5, 30), 1.25m },
            { (31, 60), 1.15m },
            { (61, 180), .95m }
        };
        /// <summary>
        /// Get Chare Rate from Length Rule Dictionary
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public async Task<decimal> GetChargeRate(int length)
        {
            foreach(KeyValuePair<(int MinLength, int MaxLength), decimal> ruleKeyValuePair in RuleDictionary)
            {
                if (length.IsInRange(ruleKeyValuePair.Key.MinLength, ruleKeyValuePair.Key.MaxLength))
                {
                    return await Task.FromResult(ruleKeyValuePair.Value);
                }
               
            }
            return 0;
        }

    }
}
