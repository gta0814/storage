var request = new XMLHttpRequest()
request.open('GET', 'https://emplistapi-258220.appspot.com/', true)
request.onload = function(){
  
}
request.send()

var data = JSON.parse(this.response)
data.forEach(jobTitle => {
  console.log(jobTitle.jobTitle)
})