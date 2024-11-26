// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*home*/
let currentIndex = 0;

function moveCarousel(direction) {
    const carousel = document.querySelector('.carousel');
    const cards = document.querySelectorAll('.card');
    const totalCards = cards.length;

    currentIndex += direction;

    // Ensure the index stays within bounds
    if (currentIndex < 0) {
        currentIndex = totalCards - 1;
    } else if (currentIndex >= totalCards) {
        currentIndex = 0;
    }

    const offset = -(currentIndex * (cards[0].offsetWidth + 20)); 
    carousel.style.transform = `translateX(${offset}px)`;
}
