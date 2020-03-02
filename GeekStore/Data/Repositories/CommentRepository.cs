using GeekStore.Data.EFContext;
using GeekStore.Data.Interfaces;
using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Repositories
{
    public class CommentRepository : IComment
    {
        private readonly DBContext _context;
        public CommentRepository(DBContext context)
        {
            _context = context;
        }
        public IEnumerable<Comment> Comments
        {
            get
            {
                return _context.Comments;
            }
        }

        public IEnumerable<Comment> GetCommentsById(int productId)
        {
            return _context.Comments.Where(x => x.ProductId == productId);
        }
    }
}
