using FosterUniteForum.Data.EntityModel;
using FosterUniteForum.Domain.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class AccessMaskService : BasicService
    {
        private AccessMask accessMask;
        private List<AccessMask> accessMaskList;

        public AccessMaskService()
        {
            accessMask = new AccessMask();
            accessMaskList = new List<AccessMask>();
        }
        public AccessMaskService(ForumDbContext context) : base(context) { }

        public bool AddAccessMask(int boardID, string maskName, int accessFlag)
        {
            AccessMask tempMask = this.GetAccessMask(maskName, boardID);

            if (tempMask == null)
            {
                accessMask.BoardId = boardID;
                accessMask.Name = maskName;
                accessMask.AccessFlag = accessFlag;
                context.AccessMask.Add(accessMask);
                context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public bool UpdateAccessMask(int accessMaskID, string maskName, int accessFlag)
        {
            accessMask = context.AccessMask.Where(m => m.Id == accessMaskID).SingleOrDefault();
            accessMask.Name = maskName;
            accessMask.AccessFlag = accessFlag;
            if (context.SaveChanges() != 0)
            {
                return true;
            }
            return false;
        }

        public List<AccessMask> GetAccessMaskList(int boardID)
        {
            return context.AccessMask.Where(m => m.BoardId == boardID).ToList();
        }

        public AccessMask GetAccessMask(string maskName, int boardID)
        {
            return context.AccessMask
                .Where(m => m.Name.Equals(maskName))
                .Where(m => m.BoardId == boardID)
                .SingleOrDefault();
        }

        public AccessMask GetAccessMaskbyID(int accessMaskID)
        {
            return context.AccessMask.Where(m => m.Id == accessMaskID).SingleOrDefault();
        }

        public int GetAccessMaskListCount()
        {
            return context.AccessMask.Count();
        }

        public int GetAccessMaskID(string maskName, int boardID)
        {
            return context.AccessMask
                .Where(m => m.Name.Equals(maskName))
                .Where(m => m.BoardId == boardID)
                .SingleOrDefault()
                .Id;
        }

        public int GetAccessMaskIDbyFlag(int accessFLag, int boardID)
        {
            if (accessFLag != 0)
            {
                return context.AccessMask
                    .Where(m => m.AccessFlag == accessFLag)
                    .Where(m => m.BoardId == boardID)
                    .SingleOrDefault()
                    .Id;
            }
            return 0;
        }

        public List<int> GetAccessMaskIDList(int boardID)
        {
            List<AccessMask> tempAccessMaskList = this.GetAccessMaskList(boardID);
            List<int> accessMaskIDList = new List<int>();
            foreach (AccessMask mask in tempAccessMaskList)
            {
                accessMaskIDList.Add(mask.Id);
            }
            return accessMaskIDList;

        }

        public void DeleteAccessMask(string maskName, int boardID)
        {
            int maskID = this.GetAccessMaskID(maskName, boardID);
            context.AccessMask.Remove(context.AccessMask.Where(m => m.Id == maskID).SingleOrDefault());
            context.SaveChangesAsync();
        }

        public void DeleteAllAccessMask(int boardID)
        {
            context.AccessMask.RemoveRange(context.AccessMask.Where(m => m.BoardId == boardID));
            context.SaveChangesAsync();
        }

        public List<string> GetAccessMaskFlag(AccessMask accessMask)
        {
            AccessMaskCollection accessMaskCollection = new AccessMaskCollection();
            int accessFlag = context.AccessMask
                    .Where(m => m.Name.Equals(accessMask.Name))
                    .Where(m => m.BoardId == accessMask.BoardId)
                    .Select(m => m.AccessFlag)
                    .SingleOrDefault();
            return accessMaskCollection.GetPairedMask(accessFlag);
        }

        public Dictionary<string, List<string>> GetMaskNameMaskFlagList(int boardID)
        {
            List<AccessMask> tempAccessMask = this.GetAccessMaskList(boardID);
            Dictionary<string, List<string>> maskNameMashFlagList = new Dictionary<string, List<string>>();

            foreach (var accessmask in tempAccessMask)
            {
                maskNameMashFlagList.Add(accessmask.Name, this.GetAccessMaskFlag(accessmask));
            }
            return maskNameMashFlagList;
        }
    }
}
