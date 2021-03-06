﻿using FubuMVC.Core.Localization;
using FubuMVC.Core.UI;
using FubuMVC.Core.View;
using Shouldly;
using HtmlTags;
using NUnit.Framework;

namespace FubuMVC.Tests.Localization
{
    [TestFixture]
    public class HtmlTagExtensionsTester
    {
        [SetUp]
        public void SetUp()
        {
            LocalizationManager.Stub("en-US");
        }

        [Test]
        public void set_localized_tag_text_by_string_token()
        {
            var token = StringToken.FromKeyString("KEY", "the text of this string token");
            new HtmlTag("a").Text(token)
                .Text().ShouldBe(token.ToString());
        }

        [Test]
        public void set_localized_attr_value_by_string_token()
        {
            var token = StringToken.FromKeyString("KEY", "the text of this string token");

            new HtmlTag("span").Attr("title", token)
                .Attr("title").ShouldBe(token.ToString());
        }

        [Test]
        public void set_the_text_by_localized_StringToken()
        {
            var token = StringToken.FromKeyString("KEY", "the localized string");
            new HtmlTag("div").TextIfEmpty(token)
                .Text().ShouldBe(token.ToString());
        }
    }
}