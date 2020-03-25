using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Platform.Core.Services.Interfaces
{
    public interface ICommentService : IServiceBase
    {
        Task Comment(int postId, string message, string ownerIdentifier);

        Task Reply(int commentId, string message, string ownerIdentifier);
    }
}
