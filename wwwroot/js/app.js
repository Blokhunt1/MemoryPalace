let slideshowInterval;

window.startBackgroundSlideshow = function() {
    const slideshow = document.getElementById('backgroundSlideshow');
    if (!slideshow) return;
    
    const images = slideshow.querySelectorAll('img');
    if (images.length === 0) return;
    
    let currentIndex = 0;
    
    // Show first image
    images[0].classList.add('active');
    
    // Clear any existing interval
    if (slideshowInterval) {
        clearInterval(slideshowInterval);
    }
    
    // Start slideshow
    slideshowInterval = setInterval(() => {
        // Remove active class from current image
        images[currentIndex].classList.remove('active');
        
        // Move to next image
        currentIndex = (currentIndex + 1) % images.length;
        
        // Add active class to new image
        images[currentIndex].classList.add('active');
    }, 4000); // Change image every 4 seconds
};

window.stopBackgroundSlideshow = function() {
    if (slideshowInterval) {
        clearInterval(slideshowInterval);
        slideshowInterval = null;
    }
};

// Sidebar toggle functionality
window.toggleSidebar = function(collapsed) {
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('mainContent');
    const toggleIcon = document.getElementById('toggleIcon');
    
    if (collapsed) {
        sidebar.classList.add('collapsed');
        mainContent.classList.add('sidebar-collapsed');
        toggleIcon.textContent = '▶';
    } else {
        sidebar.classList.remove('collapsed');
        mainContent.classList.remove('sidebar-collapsed');
        toggleIcon.textContent = '◀';
    }
};

window.toggleMobileSidebar = function(open) {
    const sidebar = document.getElementById('sidebar');
    const overlay = document.getElementById('sidebarOverlay');
    
    if (open) {
        sidebar.classList.add('mobile-show');
        overlay.classList.add('show');
    } else {
        sidebar.classList.remove('mobile-show');
        overlay.classList.remove('show');
    }
};

// Handle window resize
window.addEventListener('resize', function() {
    const sidebar = document.getElementById('sidebar');
    const overlay = document.getElementById('sidebarOverlay');
    
    if (window.innerWidth > 768) {
        // Desktop view - ensure mobile classes are removed
        sidebar.classList.remove('mobile-show');
        overlay.classList.remove('show');
    }
});

// Drag and drop functionality for loci reordering
window.makeSortable = function(elementId) {
    const element = document.getElementById(elementId);
    if (!element) return;
    
    let draggedElement = null;
    
    element.addEventListener('dragstart', function(e) {
        draggedElement = e.target;
        e.target.style.opacity = '0.5';
    });
    
    element.addEventListener('dragend', function(e) {
        e.target.style.opacity = '';
        draggedElement = null;
    });
    
    element.addEventListener('dragover', function(e) {
        e.preventDefault();
    });
    
    element.addEventListener('drop', function(e) {
        e.preventDefault();
        if (draggedElement && e.target !== draggedElement) {
            const items = [...element.children];
            const draggedIndex = items.indexOf(draggedElement);
            const targetIndex = items.indexOf(e.target);
            
            if (draggedIndex < targetIndex) {
                element.insertBefore(draggedElement, e.target.nextSibling);
            } else {
                element.insertBefore(draggedElement, e.target);
            }
            
            // Trigger update event
            const event = new CustomEvent('lociReordered', {
                detail: {
                    items: [...element.children].map(item => item.dataset.lociId)
                }
            });
            element.dispatchEvent(event);
        }
    });
};