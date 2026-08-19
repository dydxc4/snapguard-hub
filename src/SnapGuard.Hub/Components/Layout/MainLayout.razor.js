document.addEventListener('DOMContentLoaded', () => {
    const sidebarToggle = document.getElementById('sidebar-toggle');
    const sidebar = document.getElementById('sidebar');
    const overlay = document.getElementById('mobile-overlay');
    const footer = document.getElementById('footer');

    function toggleSidebar() {
        if (window.innerWidth <= 1024) {
            // Mobile behavior: show/hide completely
            sidebar.classList.toggle('sidebar-hidden');
            sidebar.classList.remove('sidebar-collapsed');
            overlay.classList.toggle('active');
        } else {
            // Desktop behavior: collapse/expand
            sidebar.classList.toggle('xl:w-64');
            sidebar.classList.toggle('sidebar-collapsed');

            // Adjust footer
            if (sidebar.classList.contains('sidebar-collapsed')) {
                footer.classList.remove('xl:left-64');
                footer.style.left = '80px';
            } else {
                footer.classList.add('xl:left-64');
                footer.style.left = '';
            }
        }
    }

    sidebarToggle.addEventListener('click', toggleSidebar);

    // Close sidebar when clicking overlay on mobile
    overlay.addEventListener('click', () => {
        if (window.innerWidth <= 1024) {
            sidebar.classList.add('sidebar-hidden');
            overlay.classList.remove('active');
        }
    });

    // Handle window resize to ensure correct state
    window.addEventListener('resize', () => {
        if (window.innerWidth > 1024) {
            overlay.classList.remove('active');
            sidebar.classList.remove('sidebar-hidden');
            if (!sidebar.classList.contains('sidebar-collapsed')) {
                sidebar.classList.add('xl:w-64');
                footer.classList.add('xl:left-64');
                footer.style.left = '';
            }
        } else {
            if (!overlay.classList.contains('active')) {
                sidebar.classList.add('sidebar-hidden');
            }
            sidebar.classList.remove('sidebar-collapsed');
            sidebar.classList.remove('xl:w-64');
            footer.classList.remove('xl:left-64');
            footer.style.left = '';
        }
    });
});
