(function () {
    "use strict";

    const page = document.querySelector(".forum-page");
    if (!page) {
        return;
    }

    const categoryButtons = Array.from(page.querySelectorAll(".forum-categories [data-category]"));
    const resetButton = page.querySelector(".forum-reset-category");
    const posts = Array.from(page.querySelectorAll("[data-post-category]"));
    const postList = page.querySelector(".forum-post-list");
    const status = page.querySelector("#forum-result-status");
    const fullFeedEmpty = page.querySelector('[data-empty-state="full-feed"]');
    const categoryEmpty = page.querySelector('[data-empty-state="category"]');
    const emptyCategoryMessage = page.querySelector("[data-empty-category-message]");

    function applyFilter(category) {
        let visibleCount = 0;

        posts.forEach(function (post) {
            const isVisible = category === "All posts" || post.dataset.postCategory === category;
            post.hidden = !isVisible;
            if (isVisible) {
                visibleCount += 1;
            }
        });

        categoryButtons.forEach(function (button) {
            const selected = button.dataset.category === category;
            button.setAttribute("aria-pressed", selected ? "true" : "false");
            button.classList.toggle("forum-category-selected", selected);
        });

        const feedIsEmpty = posts.length === 0;
        postList.hidden = feedIsEmpty || visibleCount === 0;
        if (fullFeedEmpty) {
            fullFeedEmpty.hidden = !feedIsEmpty;
        }

        const categoryIsEmpty = !feedIsEmpty && visibleCount === 0;
        categoryEmpty.hidden = !categoryIsEmpty;
        if (categoryIsEmpty) {
            emptyCategoryMessage.textContent = "No posts are available in " + category + ".";
            status.textContent = "No posts are available in " + category + ".";
        } else if (feedIsEmpty) {
            status.textContent = "No posts to show.";
        } else if (category === "All posts") {
            status.textContent = "Showing " + visibleCount + " post" + (visibleCount === 1 ? "." : "s.");
        } else {
            status.textContent = "Showing " + visibleCount + " " + category + " post" + (visibleCount === 1 ? "." : "s.");
        }
    }

    categoryButtons.forEach(function (button) {
        button.addEventListener("click", function () {
            applyFilter(button.dataset.category);
        });
    });

    if (resetButton) {
        resetButton.addEventListener("click", function () {
            applyFilter("All posts");
            const allPostsButton = categoryButtons.find(function (button) {
                return button.dataset.category === "All posts";
            });
            if (allPostsButton) {
                allPostsButton.focus();
            }
        });
    }

    applyFilter("All posts");
})();
