

// Experience POst and get data 
var sendexperiences = function (event) {
    event.preventDefault();
    //form
    var data = new FormData(event.target);
    console.log(data);
    console.log(event.target);
    $.ajax({
        url: '/Profile/AddExperience',
        type: "post",
        processData: false,
        contentType: false,
        //contentType: 'application/x-www-form-urlencoded',
        data: data,
        success: function (result) {
            getExperience();
          

        },
        failure: function (response) {
            alert("Failure");
        },
        error: function (response) {
            alert(response.error);
        },
    });
};
function getExperience() {
   
    $.ajax({
        type: 'POST',
        url: '/Profile/getExperiences',
        dataType: 'json',
        data: { id: '' },
        success: function (data) {
            var item = "";
            console.log(data);
            data = data.data;
            var from = document.getElementById("From");
            from.value = "";
            var to = document.getElementById("To");
            to.value = "";
            var CompanyName = document.getElementById("CompanyName");
            CompanyName.value = "";
            var Position = document.getElementById("Position");
            Position.value = "";

            var tbodyRef = document.getElementById('exp-table').getElementsByTagName('tbody')[0];
            var newRow = tbodyRef.insertRow();

            newRow.innerHTML= `<td><span>${data.position}</span></td>
                                        <td class="">${data.companyName}</td>
                                        <td class="">${data.from}</td>
                                        <td class="">${data.to}</td>
                                        <td class="manage">
                                            <div class="animate">
                                              
  <form method="post">
                                                    <span class="hint--top" data-hint="Delete">
                                                        <input hidden value="${data.id}" name="Id" />
                                                        <button class="delete btn btn-default" formaction="/Profile/deleteExpComplete">
                                                            <svg width="15" height="15" viewBox="0 0 16 16">
                                                                <g fill="#B2B2B2"><path d="M2.198 9.948l7.686-7.536 3.655 3.583-7.687 7.536zM5.318 13.894L1.826 10.47l-.636 1.688-1.17 3.105a.311.311 0 0 0 .074.331.325.325 0 0 0 .337.073l3.135-1.137 1.752-.636zM15.555 1.754L14.211.436c-.638-.626-1.733-.568-2.44.126l-1.434 1.405L13.99 5.55l1.433-1.405c.709-.694.767-1.768.131-2.391"></path></g>
                                                            </svg>
                                                        </button>
                                                    </span>
                                                </form>
                                               
                                            </div>
                                        </td>`;  
        },
        error: function (ex) {
            alert(ex.error)
        }
    });
    return false;
}




// Language POst and get data 
var sendLanguage = function (event) {
    event.preventDefault();
    //form
    var data = new FormData(event.target);
    console.log(data);
    console.log(event.target);
    $.ajax({
        url: '/Profile/AddLanguagecomplete',
        type: "post",
        processData: false,
        contentType: false,
        //contentType: 'application/x-www-form-urlencoded',
        data: data,
        success: function (result) {
            getLanguage();
          

        },
        failure: function (response) {
            alert("Failure");
        },
        error: function (response) {
            alert(response.error);
        },
    });
};
function getLanguage() {

    $.ajax({
        type: 'POST',
        url: '/Profile/getLanguage',
        dataType: 'json',
        data: { id: '' },
        success: function (data) {
            var item = "";
            console.log(data);
            data = data.data;
            var LanguageName = document.getElementById("LanguageName");
            LanguageName.value = "";
            var level = document.getElementById("level");
            level.value = "";
            var tbodylng = document.getElementById('lng-table').getElementsByTagName('tbody')[0];

            var newRows = tbodylng.insertRow();
            newRows.innerHTML = `<td><span>${data.languageName}</span></td>
                                        <td class="">${data.languageLevel}</td>
                                        
                                        <td class="manage">
                                            <div class="animate">
                                                <form method="post" >
                                                            <span class="hint--top" data-hint="Delete">
                                                                <input hidden value="${data.id}" name="Id" />
                                                                <button class="delete btn btn-default" formaction="/Profile/deleteLangComplete" >
                                                                    <svg width="15" height="15" viewBox="0 0 16 16">
                                                                        <g fill="#B2B2B2"><path d="M2.198 9.948l7.686-7.536 3.655 3.583-7.687 7.536zM5.318 13.894L1.826 10.47l-.636 1.688-1.17 3.105a.311.311 0 0 0 .074.331.325.325 0 0 0 .337.073l3.135-1.137 1.752-.636zM15.555 1.754L14.211.436c-.638-.626-1.733-.568-2.44.126l-1.434 1.405L13.99 5.55l1.433-1.405c.709-.694.767-1.768.131-2.391"></path></g>
                                                                    </svg>
                                                                </button>
                                                        </span>
                                                        </form>
                                            </div>
                                        </td>`;
        },
        error: function (ex) {
            alert(ex.error)
        }
    });
    return false;
}




// Skills POst and get data 
var sendSkills = function (event) {
    event.preventDefault();
    //form
    var data = new FormData(event.target);
    console.log(data);
    console.log(event.target);
    $.ajax({
        url: '/Profile/AddskillsProfile',
        type: "post",
        processData: false,
        contentType: false,
        //contentType: 'application/x-www-form-urlencoded',
        data: data,
        success: function (result) {
            getSkills();
           

        },
        failure: function (response) {
            alert("Failure");
        },
        error: function (response) {
            alert(response.error);
        },
    });
};
function getSkills() {

    $.ajax({
        type: 'POST',
        url: '/Profile/getSkills',
        dataType: 'json',
        data: { id: '' },
        success: function (data) {
            var item = "";
            console.log(data);
            data = data.data;
            var tbodyskl = document.getElementById('skltable').getElementsByTagName('tbody')[0];
            var SkillName = document.getElementById("SkillName");
            SkillName.Value = "";
            var level = document.getElementById("level");
            level.Value = "";
            var newRowskl = tbodyskl.insertRow();
            newRowskl.innerHTML = `<td><span>${data.skillName}</span></td>
                                        <td class="">${data.skillLevel}</td>
                                        
                                        <td class="manage">
                                            <div class="animate">
                                                <form method="post">
                                                            <span class="hint--top" data-hint="Delete">
                                                                <input hidden value="${data.id}" name="Id" />
                                                                <button class="delete btn btn-default" formaction="/Profile/deleteskillComplete">
                                                                    <svg width="15" height="15" viewBox="0 0 16 16">
                                                                        <g fill="#B2B2B2"><path d="M2.198 9.948l7.686-7.536 3.655 3.583-7.687 7.536zM5.318 13.894L1.826 10.47l-.636 1.688-1.17 3.105a.311.311 0 0 0 .074.331.325.325 0 0 0 .337.073l3.135-1.137 1.752-.636zM15.555 1.754L14.211.436c-.638-.626-1.733-.568-2.44.126l-1.434 1.405L13.99 5.55l1.433-1.405c.709-.694.767-1.768.131-2.391"></path></g>
                                                                    </svg>
                                                                </button>
                                                            </span>
                                                        </form>
                                            </div>
                                        </td>`;
        },
        error: function (ex) {
            alert(ex.error)
        }
    });
    return false;
}


//Delete exp, skills, Language deleteexp





///Summernote

    $(document).ready(function () {
        $('.summernote').summernote({

            tabsize: 2,
            height: 120,
            toolbar: [
                ['style', ['style']],
                ['font', ['bold', 'italic', 'underline', 'clear']],
                ['fontname', ['fontname']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'hr']],
                ['view', ['fullscreen', 'codeview']],
                ['help', ['help']]
            ],
           

        });
    });



//$(document).ready(function () {
//    $('.description').summernote({

//        tabsize: 2,
//        height: 120,
//        toolbar: [
//            ['style', ['style']],
//            ['font', ['bold', 'italic', 'underline', 'clear']],
//            ['fontname', ['fontname']],
//            ['color', ['color']],
//            ['para', ['ul', 'ol', 'paragraph']],
//            ['height', ['height']],
//            ['table', ['table']],
//            ['insert', ['link', 'picture', 'hr']],
//            ['view', ['fullscreen', 'codeview']],
//            ['help', ['help']]
//        ],


//    });
//});
//Summernote end 


//.............................................................................

////DOM elements
//const DOMstrings = {
//    stepsBtnClass: 'multisteps-form__progress-btn',
//    stepsBtns: document.querySelectorAll(`.multisteps-form__progress-btn`),
//    stepsBar: document.querySelector('.multisteps-form__progress'),
//    stepsForm: document.querySelector('.multisteps-form__form'),
//    stepsFormTextareas: document.querySelectorAll('.multisteps-form__textarea'),
//    stepFormPanelClass: 'multisteps-form__panel',
//    stepFormPanels: document.querySelectorAll('.multisteps-form__panel'),
//    stepPrevBtnClass: 'js-btn-prev',
//    stepNextBtnClass: 'js-btn-next'
//};


////remove class from a set of items
//const removeClasses = (elemSet, className) => {

//    elemSet.forEach(elem => {

//        elem.classList.remove(className);

//    });

//};

////return exect parent node of the element
//const findParent = (elem, parentClass) => {

//    let currentNode = elem;

//    while (!currentNode.classList.contains(parentClass)) {
//        currentNode = currentNode.parentNode;
//    }

//    return currentNode;

//};

////get active button step number
//const getActiveStep = elem => {
//    return Array.from(DOMstrings.stepsBtns).indexOf(elem);
//};

////set all steps before clicked (and clicked too) to active
//const setActiveStep = activeStepNum => {

//    //remove active state from all the state
//    removeClasses(DOMstrings.stepsBtns, 'js-active');

//    //set picked items to active
//    DOMstrings.stepsBtns.forEach((elem, index) => {

//        if (index <= activeStepNum) {
//            elem.classList.add('js-active');
//        }

//    });
//};

////get active panel
//const getActivePanel = () => {

//    let activePanel;

//    DOMstrings.stepFormPanels.forEach(elem => {

//        if (elem.classList.contains('js-active')) {

//            activePanel = elem;

//        }

//    });

//    return activePanel;

//};

////open active panel (and close unactive panels)
//const setActivePanel = activePanelNum => {

//    //remove active class from all the panels
//    removeClasses(DOMstrings.stepFormPanels, 'js-active');

//    //show active panel
//    DOMstrings.stepFormPanels.forEach((elem, index) => {
//        if (index === activePanelNum) {

//            elem.classList.add('js-active');

//            setFormHeight(elem);

//        }
//    });

//};

////set form height equal to current panel height
//const formHeight = activePanel => {

//    const activePanelHeight = activePanel.offsetHeight;

//    DOMstrings.stepsForm.style.height = `${activePanelHeight}px`;

//};

//const setFormHeight = () => {
//    const activePanel = getActivePanel();

//    formHeight(activePanel);
//};



////SETTING PROPER FORM HEIGHT ONLOAD
//window.addEventListener('load', setFormHeight, false);

////SETTING PROPER FORM HEIGHT ONRESIZE
//window.addEventListener('resize', setFormHeight, false);


////....................................................;




