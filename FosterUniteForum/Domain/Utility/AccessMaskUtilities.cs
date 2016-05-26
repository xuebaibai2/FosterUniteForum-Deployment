using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain.Utility
{
    enum AccessMaskUtilities 
    {
        Read,
        Post,
        Reply,
        Priority,
        Poll,
        Vote,
        Moderator,
        Edit,
        Delete,
        Upload
    }
    public class AccessMaskCollection
    {
        private Dictionary<int, string> accessMaskList = new Dictionary<int, string>();
        private int enumCount;
        private const double raisedNum = 2.0f;
        public Dictionary<int, string> getAccessMaskList()
        {
            foreach (string mask in Enum.GetNames(typeof(AccessMaskUtilities)))
            {
                int dictionaryKey = (int)Math.Pow(raisedNum, Convert.ToDouble(enumCount));
                accessMaskList.Add(dictionaryKey, mask);
                enumCount++;
            }
            return accessMaskList;
        }

        private Dictionary<int[], string> GetAccessMaskBitListInIntArray()
        {
            Dictionary<int[], string> tempAccessMaskBitList = new Dictionary<int[], string>();
            foreach (var mask in this.getAccessMaskList())
            {
                int[] dictionaryKey = this.BitConverter(mask.Key);
                tempAccessMaskBitList.Add(dictionaryKey, mask.Value);
            }
            return tempAccessMaskBitList;
        }

        private Dictionary<int, string> GetAccessMaskBitList()
        {
            Dictionary<int[], string> accessMaskBitListInIntArray = this.GetAccessMaskBitListInIntArray();
            Dictionary<int, string> accessMaskBitList = new Dictionary<int, string>();

            foreach (var mask in accessMaskBitListInIntArray)
            {
                int[] maskBit = mask.Key;
                for (int i = 0; i < maskBit.Count(); i++)
                {
                    if (maskBit.ElementAt(i) == 1)
                    {
                        accessMaskBitList.Add(i, mask.Value);
                    }
                }
            }
            return accessMaskBitList;
        }

        public List<string> GetPairedMask(int targetNumber)
        {
            List<int> targetFlagList = new List<int>();
            List<string> pairedMask = new List<string>();
            Dictionary<int, string> accessMaskBitList = this.GetAccessMaskBitList();

            int[] targetBit = this.BitConverter(targetNumber);

            for (int i = 0; i < targetBit.Count(); i++)
            {
                if (targetBit.ElementAt(i) == 1)
                {
                    targetFlagList.Add(i);
                }
            }

            foreach (var bit in targetFlagList)
            {
                foreach (var item in accessMaskBitList)
                {
                    if (item.Key == bit)
                    {
                        pairedMask.Add(item.Value);
                    }
                }
            }
            return pairedMask;
        }

        public int[] BitConverter(int intNumber)
        {
            int bitLength = Enum.GetNames(typeof(AccessMaskUtilities)).Count();
            string s = Convert.ToString(intNumber, 2); //Convert to binary in a string

            int[] bits = s.PadLeft(bitLength, '0') // Add 0's from left
                         .Select(c => int.Parse(c.ToString())) // convert each char to int
                         .ToArray(); // Convert IEnumerable from select to Array
            return bits;
        }
    }
}
