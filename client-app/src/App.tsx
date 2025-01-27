
import { useEffect, useState } from 'react'
import './App.css'
import axios from 'axios';
import { Header, List } from 'semantic-ui-react';


function App() {
 //this is to remember or store values  that change over time initially empty array
 // as soon as the app loads it talks to activities 

 const [activities,setActivities] =useState([]);

 //do something after the compopnent loads


useEffect(() =>{
  axios.get('http://localhost:5000/api/activities')
.then(response =>{
  setActivities(response.data)
})

},[])
  return (
  <div>
    <Header as ='h2' icon='users' content='Reactivities'/>
    
   <List>
      {activities.map((activity:any) =>(
        <List.Item key={activity.id}>
        {activity.title}
        </List.Item>
      ))}
    </List>

  </div> 
  )
}

export default App
