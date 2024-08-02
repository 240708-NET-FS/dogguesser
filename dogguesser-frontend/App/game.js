const options = [
    'Akita',
    'American Bulldog',
    'Poodle',
    'Afghan Hound',
    'Kai ken',
    'Siberian Husky',
    'Labrador',
    'fox'
]; //placeholder

const image = "https://upload.wikimedia.org/wikipedia/commons/d/d5/Retriever_in_water.jpg" //placeholder

const dropdown = document.getElementById('dropdown');
const searchInput = document.getElementById('search-input');
const displayImage = document.getElementById('display-image');

const populate = (options) => {
    dropdown.innerHTML = ''

    options.forEach(option => {
        const dropdownItem = document.createElement('div');
        dropdownItem.textContent = option;

        dropdownItem.addEventListener('click', () => {
            searchInput.value = option;
            dropdown.style.display = 'none';
        });
        dropdown.appendChild(dropdownItem);
    });
}

const updateImage = () => {
    if(image) {
        displayImage.src = image;
    }
}

searchInput.addEventListener('input', () => {
    const searchTerm = searchInput.value.toLowerCase();
    const filterOptions = options.filter(option => option.toLowerCase().includes(searchTerm));

    if (filterOptions.length > 0){
      populate(filterOptions); 
      dropdown.style.display = 'block'; 
    } else {
        dropdown.style.display = 'none'
    }
    
})


dropdown.addEventListener('change', () => {
    const selectedOption = dropdown.value;
    if (selectedOption) {
        searchInput.value = selectedOption;
    }
});

populate(options);
updateImage();

