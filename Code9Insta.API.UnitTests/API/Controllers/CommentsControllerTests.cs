﻿using System;
using System.Web;
using Xunit;
using Code9Insta.API.Controllers;
using Code9Insta.API.Infrastructure.Interfaces;
using FakeItEasy;
using Code9Insta.API.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using AutoMapper;
using Code9Insta.API.Core.DTO;
using System.Threading.Tasks;

namespace Code9Insta.API.UnitTests.API.Controllers
{
    public class CommentsControllerTests
    {
        private readonly IDataRepository _dataRepository = A.Fake<IDataRepository>();
        private Func<string> _getUserIdFake = () => "4ec8d89f-53d9-4cb2-b96e-2b2ec888faed";

        public CommentsControllerTests()
        {
            Mapper.Initialize(conf =>
            {
                conf.CreateMap<Comment, GetCommentDto>();
            });
        }

        [Fact]
        public void CreateCommentTest()
        {
            //arange
            var postId = Guid.NewGuid();
            var text = "This just a unit test";

            var controller = new CommentsController(_dataRepository) { _getUserId = _getUserIdFake };

            A.CallTo(() => _dataRepository.PostExists(postId)).Returns(true);
            A.CallTo(() => _dataRepository.Save()).Returns(true);

            //act
            var response = controller.CreateComment(postId, text);
            var okResult = response as ObjectResult;

            //assert
            A.CallTo(() => _dataRepository.PostExists(postId)).MustHaveHappened();
            A.CallTo(() => _dataRepository.CreateComment(A<Comment>.Ignored)).MustHaveHappened();
            A.CallTo(() => _dataRepository.Save()).MustHaveHappened();
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
      
        [Fact]
        public void DeleteCommentTest()
        {
            //arange
            var commentId = Guid.NewGuid();

            var controller = new CommentsController(_dataRepository) { _getUserId = _getUserIdFake };

            A.CallTo(() => _dataRepository.GetCommentById(commentId)).Returns(new Comment());
            A.CallTo(() => _dataRepository.Save()).Returns(true);

            //act
            var response = controller.DeleteComment(commentId);
            var okResult = response as ObjectResult;

            //assert
            A.CallTo(() => _dataRepository.GetCommentById(commentId)).MustHaveHappened();
            A.CallTo(() => _dataRepository.DeleteComment(A<Comment>.Ignored)).MustHaveHappened();
            A.CallTo(() => _dataRepository.Save()).MustHaveHappened();
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public void GetCommentsByPostIdTest()
        {
            //arange
            var postId = Guid.NewGuid();

            var controller = new CommentsController(_dataRepository) { _getUserId = _getUserIdFake };

            var comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(), 
                    PostId = postId,
                    UserId = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    Text = "just a unit test"
                }
            };

            var commentDto = new GetCommentDto
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Text = "just a unit test",
                UserId = Guid.NewGuid()
            };

            var fakeMapper = A.Fake<IMapper>();

            A.CallTo(() => _dataRepository.GetCommentsByPostId(postId)).Returns(comments);
            A.CallTo(() => fakeMapper.Map<List<GetCommentDto>>(comments)).Returns(new List<GetCommentDto>
            {
                commentDto
            }
            );

            //act
            var response = controller.GetCommentsByPostId(postId);
            var okResult = response as ObjectResult;

            //assert
            A.CallTo(() => _dataRepository.GetCommentsByPostId(postId)).MustHaveHappened();
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);


        }
    }
}
