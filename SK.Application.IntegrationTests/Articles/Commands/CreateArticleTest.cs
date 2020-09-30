﻿using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SK.Application.Articles.Commands.CreateArticle;
using SK.Application.Articles.Queries;
using SK.Application.Common.Exceptions;
using SK.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SK.Application.IntegrationTests.Articles.Commands
{
    using static Testing;

    public class CreateArticleTest : TestBase
    {
        [Test]
        public async Task ShouldCreateArticle()
        {
            //arrange
            var loggedUser = await RunAsUserAsync("scott101@localhost", "Pa$$w0rd!");
            var articleToCreate = new ArticleDto
            {
                Id = Guid.NewGuid(),
                Title = "Article Title",
                Abstract = "Article Abstract",
                Image = null,
                Content = "Article Content"
            };
            var command = new CreateArticleCommand(articleToCreate);

            //act
            var createdId = await SendAsync(command);
            var createdArticle = await FindByGuidAsync<Article>(createdId);

            //assert
            createdArticle.Id.Should().Be(createdId);
            createdArticle.Title.Should().Be(articleToCreate.Title);
            createdArticle.Abstract.Should().Be(articleToCreate.Abstract);
            createdArticle.Image.Should().BeNull();
            createdArticle.Content.Should().Be(articleToCreate.Content);
            createdArticle.CreatedBy.Should().Be(loggedUser);
            createdArticle.Created.Should().BeCloseTo(DateTime.UtcNow, 1000);
        }

        [Test]
        [TestCase(null, "Article Abstract", "Article Content")]
        [TestCase("Article Title", null, "Article Content")]
        [TestCase("Article Title", "Article Abstract", null)]
        public void ShouldRequireFieldAndThrowValidationException(string testTitle, string testAbstract, string testContent)
        {
            //arrange
            var command = new CreateArticleCommand()
            {
                Id = Guid.NewGuid(),
                Title = testTitle,
                Abstract = testAbstract,
                Image = null,
                Content = testContent
            };

            //act

            //assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }
    }
}
