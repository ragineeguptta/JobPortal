// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// 🎯 Navbar shadow on scroll
window.addEventListener("scroll", function () {
    let navbar = document.querySelector(".navbar");
    if (window.scrollY > 20) {
        navbar.style.boxShadow = "0 4px 10px rgba(0,0,0,0.2)";
    } else {
        navbar.style.boxShadow = "none";
    }
});

// Autofocus on email
document.addEventListener("DOMContentLoaded", function () {
    document.querySelector("input[name='Email']")?.focus();
});