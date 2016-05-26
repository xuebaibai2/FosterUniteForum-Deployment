using FosterUniteForum.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain.Utility
{
    public class AccessManagement
    {
        AccessMaskCollection amc;
        // readBit       =       { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
        // postBit       =       { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 };
        // replyBit      =       { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
        // priorityBit   =       { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 };
        // pollBit       =       { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 };
        // voteBit       =       { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 };
        // moderatorBit  =       { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };
        // editBit       =       { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };
        // deleteBit     =       { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
        // uploadBit     =       { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        private readonly int readIndex = 9;
        private readonly int postIndex = 8;
        private readonly int replyIndex = 7;
        private readonly int priorityIndex = 6;
        private readonly int pollIndex = 5;
        private readonly int voteIndex = 4;
        private readonly int moderatorIndex = 3;
        private readonly int editIndex = 2;
        private readonly int deleteIndex = 1;
        private readonly int uploadIndex = 0;

        public AccessManagement()
        {
            amc = new AccessMaskCollection();
        }

        public bool isReadable(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[readIndex] == 1;
        }

        public bool isPostable(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[postIndex] == 1;
        }

        public bool isReplyable(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[replyIndex] == 1;
        }

        public bool isPriority(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[priorityIndex] == 1;
        }

        public bool isPollable(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[pollIndex] == 1;
        }

        public bool isVotable(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[voteIndex] == 1;
        }

        public bool isModerator(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[moderatorIndex] == 1;
        }

        public bool isEditable(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[editIndex] == 1;
        }

        public bool isDeletable(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[deleteIndex] == 1;
        }

        public bool isUploadable(int accessFlag)
        {
            return amc.BitConverter(accessFlag)[uploadIndex] == 1;
        }
    }
}
