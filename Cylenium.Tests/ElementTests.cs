using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Cylenium.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class ElementTests : CySuite
    {
        [Test]
        [Category("element")]
        public void Click_element()
        {
            cy.Visit("https://qap.dev");
            cy.Get("[href='/about']", timeout: 5).Click();
            Assert.That(cy.Title(), Is.EqualTo("Our Vision — QA at the Point"));
        }

        [Test]
        [Category("element")]
        public void Hover_element_to_reveal_menu()
        {
            cy.Visit("https://qap.dev");
            cy.Get("[href='/about']").Hover();
            cy.Contains("Leadership", timeout: 0).Click(force: true);
            Assert.That(cy.Contains("Carlos Kidman").IsDisplayed());
            // Assert.That(cy.Contains("Carlos Kidman").Should().BeDisplayed());
        }

        [Test]
        [Category("element")]
        public void Get_element_parent_and_children()
        {
            cy.Visit("https://qap.dev");
            var elements = cy.Get("[href='/our-vision'][data-test]").Parent().Children();
            Assert.That(elements.Count, Is.EqualTo(3));
        }

        [Test]
        [Category("element")]
        public void Get_element_siblings()
        {
            cy.Visit("https://qap.dev");
            var elements = cy.Get("[href='/our-vision'][data-test]").Siblings();
            Assert.That(elements.Count, Is.EqualTo(2));
        }

        [Test]
        [Category("element")]
        public void Type_into_field()
        {
            cy.Visit("https://google.com");
            cy.Get("[name='q']").Type("puppies" + Keys.Enter);
            Assert.That(cy.Title(), Contains.Substring("puppies"));
        }

        [Test]
        [Category("element")]
        public void Submit_form()
        {
            cy.Visit("http://the-internet.herokuapp.com/login");
            cy.Get("#username").Type("tomsmith");
            cy.Get("#password").Type("SuperSecretPassword!");
            cy.Get("button[type='submit']").Submit();
            cy.Get(".flash.success").Should().BeDisplayed();
        }

        [Test]
        [Category("element")]
        public void Select_valid_text_option_from_dropdown()
        {
            cy.Visit("http://the-internet.herokuapp.com/dropdown");
            cy.Get("#dropdown").Select("Option 2");
            Assert.AreEqual(cy.Contains("Option 2").Attribute("selected"), "true");
        }

        [Test]
        [Category("element")]
        public void Select_invalid_text_option_but_valid_value_option_from_dropdown()
        {
            cy.Visit("http://the-internet.herokuapp.com/dropdown");
            cy.Get("#dropdown").Select("2");
            Assert.AreEqual(cy.Contains("Option 2").Attribute("selected"), "true");
        }

        [Test]
        [Category("element")]
        public void Select_option_from_dropdown_by_index()
        {
            cy.Visit("http://the-internet.herokuapp.com/dropdown");
            Assert.IsFalse(cy.Contains("Option 2").IsSelected());

            cy.Get("#dropdown").Select(2);
            Assert.IsTrue(cy.Contains("Option 2").IsSelected());
        }

        [Test]
        [Category("element")]
        public void Deselect_option_from_dropdown()
        {
            cy.Visit("https://www.seleniumeasy.com/test/basic-select-dropdown-demo.html");
            var dropdown = cy.Get("#multi-select");

            dropdown.Select("Ohio").Select("Texas");
            cy.Contains("Get All Selected").Click();
            Assert.That(cy.Get(".getall-selected").Text(), Does.Contain("Texas"));

            dropdown.Deselect("Texas");
            cy.Contains("Get All Selected").Click();
            Assert.That(cy.Get(".getall-selected").Text(), Does.Contain("Ohio"));
        }

        [Test]
        [Category("element")]
        public void Right_click_element_to_reveal_context_menu()
        {
            cy.Visit("http://the-internet.herokuapp.com/context_menu");
            cy.Get("#hot-spot").RightClick();
            cy.Wait().Until(drvr => drvr.SwitchTo().Alert()).Accept();
        }

        [Test]
        [Category("element")]
        public void Scroll_element_into_view()
        {
            cy.Visit("https://amazon.com");
            var backToTop = cy.Get("#navBackToTop").ScrollIntoView();
            Expect.That(backToTop.Should().BeClickable());
        }

        [Test]
        [Category("element")]
        public void Check_and_uncheck_a_checkbox()
        {
            cy.Visit("http://the-internet.herokuapp.com/checkboxes");

            var checkbox = cy.Get("[type='checkbox']");
            Assert.That(checkbox.IsChecked(), Is.False);

            checkbox.Check().Should().BeChecked();
            checkbox.Uncheck();
            Assert.That(checkbox.IsChecked(), Is.False);
        }

        [Test]
        [Category("element")]
        public void Check_an_invalid_element_throws_exception()
        {
            cy.Visit("http://the-internet.herokuapp.com/checkboxes");
            Assert.That(cy.Get("form").Check, Throws.TypeOf<UnexpectedTagNameException>());
        }

        [Test]
        [Category("element"), Category("should")]
        public void Element_not_displayed_throws_AssertionException()
        {
            cy.Visit("https://deckshop.pro");
            Assert.That(
                cy.Get("#smartHelp").Should().BeDisplayed,
                Throws.TypeOf<AssertionException>());
        }

        [Test]
        [Category("element")]
        public void Get_element_property()
        {
            cy.Visit("https://google.com");
            var innerText = cy.Get("[name='q']").Property("innerText");
            Assert.That(innerText, Is.EqualTo(""));
        }

        [Test]
        [Category("element")]
        public void Get_element_property_with_non_string_value()
        {
            cy.Visit("https://google.com");
            var field = cy.Get("[name='q']");

            var length = field.Property("maxLength");
            Assert.That(length, Is.EqualTo("2048"));

            var disabled = field.Property("disabled");
            Assert.That(disabled, Is.EqualTo("False"));

            var files = field.Property("files");
            Assert.That(files, Is.EqualTo(null));
        }

        [Test]
        [Category("element")]
        public void Element_is_enabled()
        {
            cy.Visit("http://the-internet.herokuapp.com/checkboxes");
            cy.Get("[type='checkbox']").Should().BeEnabled();
        }
    }
}
