using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class CommentRepository
    {

        private const string FilePath = "../../../Resources/Data/comments.csv";

        private readonly Serializer<Comment> serializer;

        private List<Comment> comments;

        public CommentRepository()
        {
            serializer = new Serializer<Comment>();
            comments = serializer.FromCSV(FilePath);
        }

        public List<Comment> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public Comment Save(Comment comment)
        {
            comment.Id = NextId();
            comments = serializer.FromCSV(FilePath);
            comments.Add(comment);
            serializer.ToCSV(FilePath, comments);
            return comment;
        }

        public int NextId()
        {
            comments = serializer.FromCSV(FilePath);
            if (comments.Count < 1)
            {
                return 1;
            }
            return comments.Max(c => c.Id) + 1;
        }

        public void Delete(Comment comment)
        {
            comments = serializer.FromCSV(FilePath);
            Comment founded = comments.Find(c => c.Id == comment.Id);
            comments.Remove(founded);
            serializer.ToCSV(FilePath, comments);
        }

        public Comment Update(Comment comment)
        {
            comments = serializer.FromCSV(FilePath);
            Comment current = comments.Find(c => c.Id == comment.Id);
            int index = comments.IndexOf(current);
            comments.Remove(current);
            comments.Insert(index, comment);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, comments);
            return comment;
        }

        public List<Comment> GetByUser(User user)
        {
            comments = serializer.FromCSV(FilePath);
            return comments.FindAll(c => c.User.Id == user.Id);
        }
    }
}
