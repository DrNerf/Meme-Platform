$.fn.isInViewport = function () {
    var elementTop = $(this).offset().top;
    var elementBottom = elementTop + $(this).outerHeight();
    var viewportTop = $(window).scrollTop();
    var viewportBottom = viewportTop + $(window).height();
    return elementBottom > viewportTop && elementTop < viewportBottom;
};

$(function () {
    PostsLoader.listenForNextLoad();
});

PostsLoader = {
    currentPage: 1,
    loadNextPage: function () {
        var self = PostsLoader;
        self.currentPage++;

        $.get("/Home/Posts?page=" + self.currentPage, function (response) {
            $("#posts-container").append(response);
            self.listenForLoadingImages();
            self.listenForNextLoad();
        });
    },
    listenForLoadingImages: function () {
        $('.post-image').filter(function () { return !$(this).data('loaded'); }).one('load', function () {
            var postId = $(this).attr('data-id');
            $(this).data('loaded', true);
            if (postId) {
                var overlay = $('#loading-overlay-' + postId);
                if (overlay) {
                    overlay.fadeOut(1000);
                }
            }
        }).each(function () {
            if (this.complete) $(this).load();
        });
    },
    listenForNextLoad: function () {
        $(window).on('scroll', function (event) {
            if ($('.progress-spinner').isInViewport()) {
                console.log("Reached end of posts page, loading more.");
                $(this).off('scroll');
                PostsLoader.loadNextPage();
            }
        });
    }
};

PostsLoader.listenForLoadingImages();