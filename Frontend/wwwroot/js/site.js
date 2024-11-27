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


//acount page, returns btn//
// Wait for the DOM to load fully
document.addEventListener('DOMContentLoaded', function () {
    // Add event listener to each 'Return' button
    document.querySelectorAll('.return-btn').forEach(button => {
        button.addEventListener('click', function () {
            const row = this.closest('tr'); // Find the row that the button is in
            const bookTitle = row.querySelector('td:first-child').textContent.trim(); // Get the book title
            const returnDate = row.querySelector('td:nth-child(3)').textContent.trim(); // Get the return date

            // Change button to 'Returned' and update the status in the table
            row.querySelector('td:nth-child(4)').innerHTML = '<span>Returned</span>'; // Update the status
            this.disabled = true;  // Disable the button after it's clicked
            this.textContent = 'Returned'; // Optionally, change button text

            // If you want to send an update to the server (optional)
            fetch('/Account/ReturnBook', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ bookTitle, returnDate })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        console.log('Book returned successfully');
                    } else {
                        alert('Error marking the book as returned');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('Error communicating with the server');
                });
        });
    });
});

//borrow btn
document.getElementById('borrow-btn').addEventListener('click', function () {
    const bookTitle = document.querySelector('h2').textContent.trim();  // Get the book title
    const author = document.querySelector('p strong').nextElementSibling.textContent.trim(); // Get the author
    const returnDate = getFutureDate(); // For example, borrow it for 10 days from now

    // Send a request to the server to borrow the book and add it to the Account Page
    fetch('/Account/BorrowBook', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ bookTitle, author, returnDate })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // Add the book to the borrowed books table in the Account Page (this is the Account Page table)
                const borrowedBooksTable = document.getElementById('borrowed-books-table').getElementsByTagName('tbody')[0];
                const newRow = borrowedBooksTable.insertRow();
                newRow.innerHTML = `
                <td>${bookTitle}</td>
                <td>${author}</td>
                <td>${returnDate}</td>
                <td><button class="return-btn">Return</button></td>
            `;
                // Optionally, disable the button after borrowing
                document.getElementById('borrow-btn').disabled = true;
                document.getElementById('borrow-btn').textContent = 'Borrowed';
            } else {
                alert('Failed to borrow the book');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Error borrowing the book');
        });
});

// Helper function to get the return date (e.g., 10 days from now)
function getFutureDate() {
    const today = new Date();
    today.setDate(today.getDate() + 10);  // Set return date to 10 days from now
    return today.toISOString().split('T')[0]; // Format as YYYY-MM-DD
}
