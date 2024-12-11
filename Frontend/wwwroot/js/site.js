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
document.addEventListener('DOMContentLoaded', function () {
    // Sample in-memory data for reading history and transactions
    let readingHistory = [
        { bookId: 1, title: "To Kill a Mockingbird", status: "Not Returned" },
        { bookId: 2, title: "Pride and Prejudice", status: "Not Returned" }
    ];

    let transactions = [
        { transactionId: 1, mediaItemId: 1001, borrowDate: "12/01/2024", dueDate: "12/15/2024", status: "Borrowed" },
        { transactionId: 2, mediaItemId: 1002, borrowDate: "12/02/2024", dueDate: "12/16/2024", status: "Borrowed" }
    ];

    // Function to render the reading history
    function renderReadingHistory() {
        const readingHistoryContainer = document.querySelector('.reading-history ul');
        readingHistoryContainer.innerHTML = ''; // Clear existing list
        readingHistory.forEach(book => {
            const li = document.createElement('li');
            li.id = `book-${book.bookId}`;
            li.innerHTML = `${book.title} - <button class="return-btn" data-book-id="${book.bookId}">${book.status === 'Not Returned' ? 'Return' : 'Returned'}</button>`;
            readingHistoryContainer.appendChild(li);
        });
    }

    // Function to render the transactions table (if necessary)
    function renderTransactionTable() {
        const transactionTableBody = document.querySelector('.borrowed-books table tbody');
        transactionTableBody.innerHTML = ''; // Clear existing table
        transactions.forEach(transaction => {
            const tr = document.createElement('tr');
            tr.id = `transaction-${transaction.transactionId}`;
            tr.innerHTML = `
                <td>${transaction.transactionId}</td>
                <td>${transaction.mediaItemId}</td>
                <td>${transaction.borrowDate}</td>
                <td>${transaction.dueDate}</td>
                <td class="status">${transaction.status}</td>
            `;
            transactionTableBody.appendChild(tr);
        });
    }

    // Initial render
    renderReadingHistory();
    renderTransactionTable();

    // Add event listener to each 'Return' button
    document.querySelectorAll('.return-btn').forEach(button => {
        button.addEventListener('click', function () {
            const bookId = this.getAttribute('data-book-id');  // Get the book ID

            // Find the book in the reading history and update its status
            const book = readingHistory.find(b => b.bookId == bookId);
            if (book && book.status === 'Not Returned') {
                book.status = 'Returned'; // Mark the book as returned
            }

            // Find the corresponding transaction and update its status
            const transaction = transactions.find(t => t.transactionId == bookId);
            if (transaction && transaction.status === 'Borrowed') {
                transaction.status = 'Returned'; // Mark the transaction as returned
            }

            // Update the DOM (UI)
            renderReadingHistory();  // Re-render reading history
            renderTransactionTable();  // Re-render transaction table

            // Disable the button and change text to 'Returned'
            this.disabled = true;
            this.textContent = 'Done';

            console.log(`${book.title} has been marked as returned.`);
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
