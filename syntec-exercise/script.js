//work with html element
const employeePage = document.getElementById('employees')
employeePage.setAttribute("class", "media")

//get api data to html
var request = new XMLHttpRequest()
request.open('GET', 'https://emplistapi-258220.appspot.com/', true)
request.onload = function(){
  var data = JSON.parse(this.response)
  data.forEach(person => {
    if(person.jobTitle != null && person.name.first !=null && person.name.last != null)
    {
      var card = document.createElement('div')
      card.setAttribute('class', 'columns card')
      
      if (person.photoURL != null) {
        //img part
        var figure = document.createElement('div')
        figure.setAttribute('class', 'column is-one-fifth')
        var img = document.createElement('img')
        img.setAttribute('class', 'list-image')
        img.src = person.photoURL //data
        figure.appendChild(img)
      }
      else{
        var figure = document.createElement('div')
        figure.setAttribute('class', 'column is-one-fifth')
        var img = document.createElement('img')
        img.setAttribute('class', 'list-image')
        img.src = 'https://upload.wikimedia.org/wikipedia/commons/7/7c/User_font_awesome.svg'
        figure.appendChild(img)
      }
      

      //Employee Name
      var content = document.createElement('div')
      content.setAttribute('class', 'column')
      var subContent = document.createElement('p')
      //subContent.setAttribute('class', 'content')
      var name = document.createElement('h5')
      name.textContent = person.name.first + ' ' + person.name.last //data
      

      //Employee job
      var jobTitle = document.createElement('p')
      jobTitle.textContent = person.jobTitle //data
      subContent.appendChild(name)
      subContent.appendChild(jobTitle)

      content.appendChild(subContent)

      employeePage.appendChild(card)
      card.appendChild(figure)
      card.appendChild(content)
    }
})
}
request.send()

document.getElementById('Add-Employee-btn').addEventListener("click", openModal)
function openModal() {
  var modal = document.getElementById('modal')
  modal.classList.add('is-active')
}

var deleteBtn = document.getElementsByClassName('close-modal')
for (let index = 0; index < deleteBtn.length; index++) {
  deleteBtn[index].addEventListener("click", closeModal)
  
}

function closeModal(){
  var modal = document.getElementById('modal')
  modal.classList.remove('is-active')
}