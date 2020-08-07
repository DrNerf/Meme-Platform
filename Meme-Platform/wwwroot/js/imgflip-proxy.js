ImgFlipProxy = {
    unusedElements: [
        '.head',
        '#panel-back',
        '#mm-recs-wrap',
        '#gen-qa',
        '#pro-basic-popup-wrap',
        '#footer',
        '.feedback',
        '.gen-no-watermark-wrap',
        '.gen-login-wrap',
        '#arc-widget-container',
        '.mm-generate.b.but',
        '.gen-login-wrap'
    ],
    init: function () {
        // Hide some elements we don't need and insert our custom stuff.
        var _this = this;
        $('#mp-generator>iframe').on('load', function () {
            var $this = $(this);

            $(_this.unusedElements.join(','), $this.contents()).hide();
            $('a[href="/gif-maker"]', $this.contents()).parent().hide();
            $('body', $this.contents()).css({ background: 'transparent' });
            $('.check-wrap.gen-private', $this.contents()).click().hide();
            $($('#memegen-additional-dom').html()).insertBefore($('.gen-wrap', $this.contents()));
            $('.memegen-upload', $this.contents()).on('click', _this.uploadMeme.bind(_this));

            _this.hideBlur();
        });
    },
    hideBlur: function () {
        $('#blur-overlay').fadeOut(1500);
    },
    showBlur: function () {
        $('#blur-overlay').fadeIn(500);
    },
    uploadMeme: function () {
        var _this = this;
        var $iframe = $('#mp-generator>iframe');
        var title = $('#memegen-title', $iframe.contents()).val();
        var isNSFW = $('.memegen-nsfw', $iframe.contents()).hasClass('checked');
        if (!title || title === '') {
            $('#no-title-toast').toast('show');
        } else {
            this.showBlur();
            $('.mm-generate.b.but', $iframe.contents()).click();
            // Wait for ImgFlip to process the image and allow downloading.
            var poller = setInterval(function () {
                var $downloadBtn = $('a.img-download.l.but', $iframe.contents());
                if ($downloadBtn.length) {
                    clearInterval(poller);
                    _this.uploadMemeImage(title, isNSFW, $downloadBtn.attr('href'));
                }
            }, 200);
        }
    },
    uploadMemeImage: function (title, isNSFW, imageUrl) {
        var _this = this;
        $.ajax({
            type: "POST",
            url: '/Posts/UploadGeneratedMeme',
            data: {
                title: title,
                isNSFW: isNSFW,
                imageUrl: imageUrl
            },
            success: function () {
                window.location.href = '/Home/Index';
            },
            error: function () {
                $('#server-error-toast').toast('show');
            },
            complete: function () {
                _this.hideBlur();
            }
        });
    }
}