let openRect = popmotion.svg(document.querySelector('#open rect'));
popmotion.timeline([
    popmotion.tween({
        duration: 500,
        //ease: easing.easeOut,
        onUpdate: function(x) {
            openRect.set('x', 100 - (x * 100));
            openRect.set('width', 300 + (x * 275));
        }
    })
]).start();