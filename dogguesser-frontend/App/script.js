const options = [
    'dog 1',
    'dog 2',
    'dog 3',
    'cat 1',
    'cat 2'
]; //placeholder

const image = "https://upload.wikimedia.org/wikipedia/commons/d/d5/Retriever_in_water.jpg" //placeholder

const dropdown = document.getElementById('dropdown');
const searchInput = document.getElementById('search-input');
const displayImage = document.getElementById('display-image');

const populate = (options) => {
    dropdown.innerHTML = '<option value="">select a doggo</option>'

    options.forEach(option => {
        const dropdownItem = document.createElement('option');
        dropdownItem.value = option;
        dropdownItem.textContent = option;
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
    populate(filterOptions);
})


dropdown.addEventListener('change', () => {
    const selectedOption = dropdown.value;
    if (selectedOption) {
        searchInput.value = selectedOption;
    }
});

populate(options);
updateImage();

