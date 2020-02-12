using Meme_Platform.Core.Models;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using VoteType = Meme_Platform.DAL.Entities.VoteType;

namespace Meme_Platform.Core.Transformers.Classes
{
    internal class VoteToVoteModelTransformer : ITransformer<Vote, VoteModel>
    {
        private readonly ITransformer<Profile, ProfileModel> profileTransformer;

        public VoteToVoteModelTransformer(ITransformer<Profile, ProfileModel> profileTransformer)
        {
            this.profileTransformer = profileTransformer;
        }

        public VoteModel Transform(Vote source)
        {
            return new VoteModel
            {
                Voter = profileTransformer.Transform(source.Voter),
                Type = convertVoteType(source.Type)
            };
        }

        private Models.VoteType convertVoteType(VoteType voteType)
        {
            switch (voteType)
            {
                case VoteType.Up:
                    return Models.VoteType.Up;
                case VoteType.Down:
                    return Models.VoteType.Down;
                default:
                    throw new ArgumentException("Ey yo WTF!", nameof(voteType));
            }
        }
    }
}
