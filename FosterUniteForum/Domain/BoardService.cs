using FosterUniteForum.Data.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class BoardService : BasicService
    {
        private Board board;
        private List<Board> boardList;
        private List<string> boardNameList;
        private List<string> boardDesList;
        private List<int> boardIDList;
        private int boardID;
        public BoardService()
        {
            board = new Board();
            boardList = new List<Board>();
        }
        public BoardService(ForumDbContext context) : base(context) { }

        public void AddBoard(string boardName, string boardDescription)
        {
            board.Name = boardName;
            board.Description = boardDescription;
            context.Board.Add(board);
            context.SaveChanges();
        }
        public List<Board> GetBoardList()
        {
            //int number = context.Board.Count();
            return this.boardList = context.Board.ToList();
        }
        public List<string> GetBoardNameeList()
        {
            return this.boardNameList = context.Board.Select(m => m.Name).ToList();
        }
        public List<string> GetBoardDescriptionList()
        {
            return this.boardDesList = context.Board.Select(m => m.Description).ToList();
        }
        public int GetBoardListCount()
        {
            return context.Board.Count();
        }
        public int GetBoardID(string boardName)
        {
            List<string> tempNameList = this.GetBoardNameeList();
            if (tempNameList.Contains(boardName))
            return this.boardID = context.Board.Where(m => m.Name.Equals(boardName))
                .Select(m => m.Id).SingleOrDefault();
            else
                return 0;
        }
        public Board GetBoard(int boardID)
        {
            return context.Board.Where(m => m.Id == boardID).Single();
        }
        public void DeleteBoard(string boardName)
        {
            AccessMaskService ams = new AccessMaskService();
            int boardID = this.GetBoardID(boardName);
            ams.DeleteAllAccessMask(boardID);
            context.Board.Remove(context.Board.Where(m => m.Id == boardID).Single());
            context.SaveChanges();
        }
        public List<int> GetBoardIDList()
        {
            return this.boardIDList = context.Board.Select(m => m.Id).ToList();
        }
        public void UpdateBoard(int boardID, string boardName, string boardDescription)
        {
            board = context.Board.Where(m => m.Id == boardID).Single();
            board.Name = boardName;
            board.Description = boardDescription;
            context.SaveChanges();
        }
    }
}
