(function () {
	"use strict";

	window.onload = function () {

		// Header Sticky
		const getHeaderId = document.getElementById("header-area");
		if (getHeaderId) {
			window.addEventListener('scroll', event => {
				const height = 150;
				const { scrollTop } = event.target.scrollingElement;
				document.querySelector('#header-area').classList.toggle('sticky', scrollTop >= height);
			});
		}

		// Header Sticky
		const getNavbarId = document.getElementById("navbar");
		if (getNavbarId) {
			window.addEventListener('scroll', event => {
				const height = 150;
				const { scrollTop } = event.target.scrollingElement;
				document.querySelector('#navbar').classList.toggle('sticky', scrollTop >= height);
			});
		}
	};

	// Menu JS
	let menu, animate;

	(function () {
		// Initialize menu
		let layoutMenuEl = document.querySelectorAll('#layout-menu');
		layoutMenuEl.forEach(function (element) {
			menu = new Menu(element, {
				orientation: 'vertical',
				closeChildren: false
			});
			// Change parameter to true if you want scroll animation
			window.Helpers.scrollToActive((animate = false));
			window.Helpers.mainMenu = menu;
		});

	})();

	// Sidebar Burger Button
	const getSidebarBurgerMenuId = document.getElementById('sidebar-burger-menu');
	if (getSidebarBurgerMenuId) {
		const switchtoggle = document.querySelector(".sidebar-burger-menu");
		switchtoggle.addEventListener("click", function () {
			if (document.body.getAttribute("sidebar-data-theme") === "sidebar-hide") {
				document.body.setAttribute("sidebar-data-theme", "sidebar-show");
			} else {
				document.body.setAttribute("sidebar-data-theme", "sidebar-hide");
			}
		});
	}

	// Sidebar Burger Close Button
	const getSidebarBurgerMenuCloseId = document.getElementById('sidebar-burger-menu-close');
	if (getSidebarBurgerMenuCloseId) {
		const switchtoggle = document.querySelector(".sidebar-burger-menu-close");
		switchtoggle.addEventListener("click", function () {
			if (document.body.getAttribute("sidebar-data-theme") === "sidebar-hide") {
				document.body.setAttribute("sidebar-data-theme", "sidebar-show");
			} else {
				document.body.setAttribute("sidebar-data-theme", "sidebar-hide");
			}
		});
	}

	// Header Burger Button
	const getHeaderBurgerMenuId = document.getElementById('header-burger-menu');
	if (getHeaderBurgerMenuId) {
		const switchtoggle = document.querySelector(".header-burger-menu");
		switchtoggle.addEventListener("click", function () {
			if (document.body.getAttribute("sidebar-data-theme") === "sidebar-hide") {
				document.body.setAttribute("sidebar-data-theme", "sidebar-show");
			} else {
				document.body.setAttribute("sidebar-data-theme", "sidebar-hide");
			}
		});
	}
 
	// data picker
	$(function(){
		$('#YardInDate').datepicker();
		$('#YardOutDate').datepicker();
	});

	// Init BS Tooltip
	const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
	tooltipTriggerList.map(function (tooltipTriggerEl) {
		return new bootstrap.Tooltip(tooltipTriggerEl);
	});

	// image light box
	const galleryItem = document.getElementsByClassName("flex-item");
    const lightBoxContainer = document.createElement("div");
    const lightBoxContent = document.createElement("div");
    const lightBoxImg = document.createElement("img");
    const lightBoxPrev = document.createElement("div");
    const lightBoxNext = document.createElement("div");

    lightBoxContainer.classList.add("lightbox");
    lightBoxContent.classList.add("lightbox-content");
    lightBoxPrev.classList.add("ti", "ti-arrow-left", "lightbox-prev");
    lightBoxNext.classList.add("ti", "ti-arrow-right", "lightbox-next");

    lightBoxContainer.appendChild(lightBoxContent);
    lightBoxContent.appendChild(lightBoxImg);
    lightBoxContent.appendChild(lightBoxPrev);
    lightBoxContent.appendChild(lightBoxNext);

    document.body.appendChild(lightBoxContainer);

    let index = 1;

    function showLightBox(n) {
        if (n > galleryItem.length) {
            index = 1;
        } else if (n < 1) {
            index = galleryItem.length;
        }
        let imageLocation = galleryItem[index-1].children[0].getAttribute("src");
        lightBoxImg.setAttribute("src", imageLocation);
    }

    function currentImage() {
        lightBoxContainer.style.display = "block";

        let imageIndex = parseInt(this.getAttribute("data-index"));
        showLightBox(index = imageIndex);
    }
    for (let i = 0; i < galleryItem.length; i++) {
        galleryItem[i].addEventListener("click", currentImage);
    }

    function slideImage(n) {
        showLightBox(index += n);
    }
    function prevImage() {
        slideImage(-1);
    }
    function nextImage() {
        slideImage(1);
    }
    lightBoxPrev.addEventListener("click", prevImage);
    lightBoxNext.addEventListener("click", nextImage);

    function closeLightBox() {
        if (this === event.target) {
            lightBoxContainer.style.display = "none";
        }
    }
    lightBoxContainer.addEventListener("click", closeLightBox);


	// Right Sidebar
	const getRightSidebarId = document.getElementById('right-sidebar');
	if (getRightSidebarId) {
		const switchtoggle = document.querySelector(".right-sidebar");
		const savedTheme = localStorage.getItem("fila_theme");
		if (savedTheme) {
			document.body.setAttribute("right-sidebar-data-theme", savedTheme);
		}
		switchtoggle.addEventListener("click", function () {
			if (document.body.getAttribute("right-sidebar-data-theme") === "right-sidebar-normal") {
				document.body.setAttribute("right-sidebar-data-theme", "right-sidebar-right");
				localStorage.setItem("fila_theme", "right-sidebar-right");
			} else {
				document.body.setAttribute("right-sidebar-data-theme", "right-sidebar-normal");
				localStorage.setItem("fila_theme", "right-sidebar-normal");
			}
		});
	}

	// Menu Left Right Slide JS
	const geMenuLeftRightSlideId = document.getElementById('menu');
	if (geMenuLeftRightSlideId) {

		document.addEventListener("DOMContentLoaded", () => {
			const menuItems = document.querySelectorAll("#menu > li");
			const prevBtn = document.getElementById("prev-btn");
			const nextBtn = document.getElementById("next-btn");
			let itemsPerPage = 8; // Default value
			let currentIndex = 0;
		
			// Function to update menu visibility
			function updateMenu() {
				menuItems.forEach((item, index) => {
					item.style.display =
						index >= currentIndex && index < currentIndex + itemsPerPage
							? "block"
							: "none";
				});
		
				prevBtn.disabled = currentIndex === 0;
				nextBtn.disabled = currentIndex + itemsPerPage >= menuItems.length;
			}
		
			// Function to update itemsPerPage based on screen size
			function updateItemsPerPage() {
				if (window.matchMedia("(max-width: 992px)").matches) {
					itemsPerPage = 7; // Show 1 item for small screens
				} else if (window.matchMedia("(max-width: 1024px)").matches) {
					itemsPerPage = 7; // Show 2 items for medium screens
				} else {
					itemsPerPage = 4; // Show 3 items for large screens
				}
				currentIndex = 0; // Reset index when itemsPerPage changes
				updateMenu();
			}
		
			// Event listeners for buttons
			prevBtn.addEventListener("click", () => {
				if (currentIndex > 0) {
					currentIndex -= 1; // Move back by one item
					updateMenu();
				}
			});
		
			nextBtn.addEventListener("click", () => {
				if (currentIndex + itemsPerPage < menuItems.length) {
					currentIndex += 1; // Move forward by one item
					updateMenu();
				}
			});
		
			// Add event listener for screen size changes
			window.addEventListener("resize", updateItemsPerPage);
		
			// Initial setup
			updateItemsPerPage();
		});
	}

	// menu link active
	$(document).ready(function () {
		// Remove active from all menu-links
		$(".menu-link").removeClass("active");

		// Add active on click
		$(".menu-link").on("click", function () {
			$(".menu-link").removeClass("active");
			$(this).addClass("active");
		});

		// Auto add active based on current page URL
		var currentPage = window.location.pathname.split("/").pop(); // e.g. inventory-status.html

		$(".menu-link").each(function () {
			var linkPage = $(this).attr("href");

			if (linkPage === currentPage) {
				$(this).addClass("active");

				// Also open sub-menu if inside one
				$(this).closest(".menu-sub").prev(".menu-link.menu-toggle").addClass("active");
			}
		});
	});


	// light dark theme
	// Page load par theme apply karo
	(function () {
		const savedTheme = localStorage.getItem("theme") || "light";
		document.documentElement.className = savedTheme;
	})();

	// Global function (IMPORTANT)
	window.setTheme = function (theme) {
		document.documentElement.className = theme;
		localStorage.setItem("theme", theme);
	};

})();

