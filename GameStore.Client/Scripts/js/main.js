const
    CARD_ZOOM_VALUE = '105%',
    CARD_DEFAULT_VALUE = '100%',
    SEARCH_TIME_OUT = 700,
    SEARCH_REQUEST_URL = '/Games/Search/?data=[data]',
    FILTER_REQUEST_URL = '/Games/Index/?filter=[data]',
    ARCHIVE_LIST_REQUEST_URL = '/Games/ArchiveList/',
    ARCHIVE_REQUEST_URL = '/Games/Archive/[id]',
    RECOVER_REQUEST_URL = '/Games/Recover/[id]',
    DELETE_REQUEST_URL = '/Games/Delete/[id]';


//-----------------Events----------------------
document.body.onload = window.onresize = scaleCards;
document.querySelector('#search__input').addEventListener('keyup', searchInputHandler);
let navbarBtns = document.querySelectorAll('button[data-gs-navbar-toggler]');

let archiveBtns = document.querySelectorAll('a[data-gs-archive-btn], button[data-gs-archive-btn]');
for (let item of archiveBtns) {
    item.onclick = archiveClick;
}


//Inserting data-gs-navbar-toggler buttons to navbar
let navbarBtnList = document.querySelector('#navbar__buttons')
for (let item of navbarBtns) {
    item.parentElement.removeChild(item);
    let liElem = document.createElement('li');
    liElem.appendChild(item);
    navbarBtnList.prepend(liElem);
}

var cardImages = null;

function updateCards() {
    cardImages = document.querySelectorAll('.card-img-top');

    for (let item of cardImages) {
        let imageSrc = item.style
            .backgroundImage;
        let image = document.createElement('img');
        image.src = imageSrc;
        if (image.width >= image.height) {
            item.style.backgroundSize = 'auto 100%';
        } else {
            item.style.backgroundSize = '100% auto';
        }
        item.onmouseenter = () => {
            item.style.backgroundSize = item.style.backgroundSize.replace(CARD_DEFAULT_VALUE, CARD_ZOOM_VALUE);
        }
        item.onmouseleave = () => {
            item.style.backgroundSize = item.style.backgroundSize.replace(CARD_ZOOM_VALUE, CARD_DEFAULT_VALUE);
        }
    }
}

updateCards();

mainAuth();


//Cards initialising (scale and zoom) 


function scaleCards() {
    for (let item of cardImages) {
        item.style.height = item.offsetWidth * 1.4 + 'px';
    }
}

// #region Game deleting

async function archiveClick(e) {
    let btn = e.target;
    if (btn.hasAttribute('data-target-id') && confirm('Are you sure deleting this game?')) {
        let id = btn.getAttribute('data-target-id');
        let reply = await fetch(ARCHIVE_REQUEST_URL.replace('[id]', id));
        let answ = await reply.text();
        if (answ === 'ok') {
            let elem = btn;
            while (elem.parentElement.id != 'cards__list') {
                elem = elem.parentElement;
            }
            elem.parentElement.removeChild(elem);
        }
    }
}

// #endregion 


//#region Search

let timeoutKeeper = null;

function searchInputHandler(e) {
    let field = e.target;
    if (timeoutKeeper != null) {
        console.log('cleared');
        clearTimeout(timeoutKeeper);
    }
    if (!field.value.match(/^ *$/g)) {
        console.log('added new');
        timeoutKeeper = setTimeout(() => {
            searchRequest(field.value);
            timeoutKeeper = null;
        }, SEARCH_TIME_OUT);
    } else {
        setSearchResults(null);
    }
}

async function searchRequest(data) {
    try {
        let responce = await fetch(SEARCH_REQUEST_URL.replace('[data]', data));
        let text = await responce.text();
        if (!text.match('n_found')) {
            setSearchResults(text);
            return;
        }
    } catch (ex) {}
    setSearchResults('Can\'t find any game');
}

function setSearchResults(data) {
    let resultsField = document.querySelector('#search__results__field');
    if (data === undefined || data === null) {
        resultsField.parentElement.classList.add('collapse');
    } else {
        resultsField.parentElement.classList.remove('collapse');
        resultsField.innerHTML = data;

    }
}

//#endregion

//#region Sidebar

//init----------



const filterAttr = {
    genre: 'data-gs-genre-btn',
    developer: 'data-gs-developer-btn',
    sortType: 'data-gs-sort-btn',
    sortTypeName: 'data-gs-sort-btn-name'
};


var __init__sort__items = false;

function updateFiltersClick() {
    var sidebarGenres = document.querySelectorAll('#genreListFilter>button');
    var sidebarDevelopers = document.querySelectorAll('#developerListFilter>button');
    var sidebarSortTypes = document.querySelectorAll(`[${filterAttr.sortType}]`);
    for (let item of sidebarGenres) {
        item.addEventListener('click', filterClick);
    }

    for (let item of sidebarDevelopers) {
        item.addEventListener('click', filterClick);
    }

    for (let item of sidebarSortTypes) {
        item.addEventListener('click', sortTypeClick);

        if (__init__sort__items === false) {
            let name = item.getAttribute(filterAttr.sortTypeName).split('-');
            switch(name[1]){
                case 'asc':
                    item.innerHTML += '	&#8593;';
                    break;
                case 'desc':
                    item.innerHTML += '	&#8595;';
                    break;
            }
            switch(name[0]){
                case 'description':
                case 'image':
                case 'id':
                    item.parentElement.removeChild(item);
                    break;
            }
        }
    }
    __init__sort__items = true;

}

updateFiltersClick();

let filter = {
    genres: [],
    developers: [],
    sortKeyWord: ''
};


function sortTypeClick(e){
    filter.sortKeyWord = e.target.getAttribute(filterAttr.sortTypeName);
    document.querySelector('#sortTypesBtn').innerHTML = e.target.innerHTML;
    updateGames();
}


function filterClick(e) {
    let button = e.target;
    if (button.hasAttribute(filterAttr.genre)) {
        if (!button.classList.contains('sidebar__btn__selected')) {
            button.classList.add('sidebar__btn__selected');
            filter.genres.push(button.innerHTML);
        } else {
            button.classList.remove('sidebar__btn__selected');
            filter.genres.splice(filter.genres.indexOf(button.innerHTML), 1);
        }
    } else if (button.hasAttribute(filterAttr.developer)) {
        if (!button.classList.contains('sidebar__btn__selected')) {
            button.classList.add('sidebar__btn__selected');
            filter.developers.push(button.innerHTML);
        } else {
            button.classList.remove('sidebar__btn__selected');
            filter.developers.splice(filter.developers.indexOf(button.innerHTML), 1);
        }
    }
    document.querySelector('#sidebar__main').setAttribute('disabled', '');
    updateGames();
}

function updateGames() {
    requestFilter().then(() => {
        updateCards();
        scaleCards();

        let archiveBtns = document.querySelectorAll('a[data-gs-archive-btn], button[data-gs-archive-btn]');
        for (let item of archiveBtns) {
            item.onclick = archiveClick;
        }

    });


}



async function requestFilter() {
    let json = JSON.stringify(filter);
    let res = await fetch(FILTER_REQUEST_URL.replace('[data]', json), { method: 'GET' });
    let data = (await res.text()).split('<!--CARD-->')[1].split('<!--/CARD-->')[0];
    // console.log(data);
    document.querySelector('#cards__list').innerHTML = data;
}

let oldWindowWidth = window.innerWidth;

function initFilterView() {
    
    if (window.innerWidth < 992) {
        let data = document.querySelector('#sidebar__main').innerHTML;
        document.querySelector('#filter__content').innerHTML = data;
        document.querySelector('#sidebar__main').innerHTML = '';
        oldWindowWidth = window.innerWidth;
        updateFiltersClick();
    }
}

function changeFilterView() {
    if (window.innerWidth < 992 && oldWindowWidth >= 992) {
        let data = document.querySelector('#sidebar__main').innerHTML;
        document.querySelector('#filter__content').innerHTML = data;
        document.querySelector('#sidebar__main').innerHTML = '';
        oldWindowWidth = window.innerWidth;
        updateFiltersClick();
    } else if (window.innerWidth >= 992 && oldWindowWidth < 992) {
        let data = document.querySelector('#filter__content').innerHTML;
        document.querySelector('#sidebar__main').innerHTML = data;
        document.querySelector('#filter__content').innerHTML = '';
        oldWindowWidth = window.innerWidth;
        updateFiltersClick();
    }
}

window.addEventListener('resize', changeFilterView);
initFilterView();

//endinit-------

//#endregion

//#region Archive

function updateArchiveList() {
    var recoverBtns = document.querySelectorAll('button[data-gs-recover-btn], a[data-gs-recover-btn]');
    var deleteBtns = document.querySelectorAll('button[data-gs-delete-btn], a[data-gs-delete-btn]');

    for (let item of recoverBtns) {
        item.addEventListener('click', recoverRequest);
    }

    for (let item of deleteBtns) {
        item.addEventListener('click', deleteRequest);
    }
}

updateArchiveList();


document.querySelector('#archive__btn').addEventListener('click', async(e) => {
    let res = await fetch(ARCHIVE_LIST_REQUEST_URL);
    let data = await res.text();
    document.querySelector('#archive__list').innerHTML = data;
    updateArchiveList();
});

async function recoverRequest(e) {
    let target = e.target;
    let res = await fetch(RECOVER_REQUEST_URL.replace('[id]', e.target.getAttribute('data-target-id')));
    let data = await res.text();
    if (data === 'ok') {
        let parent = document.querySelector('#archive__list');
        while (target.parentElement != parent)
            target = target.parentElement;
        target.parentElement.removeChild(target);
        updateGames();
    }

}

async function deleteRequest(e) {
    let target = e.target;
    let res = await fetch(DELETE_REQUEST_URL.replace('[id]', e.target.getAttribute('data-target-id')));
    let data = await res.text();
    if (data === 'ok') {
        let parent = document.querySelector('#archive__list');
        while (target.parentElement != parent)
            target = target.parentElement;
        target.parentElement.removeChild(target);
    }
}

//#endregion


//#region Auth

function getAuthToken() {

}

function mainAuth() {

}

//#endregion