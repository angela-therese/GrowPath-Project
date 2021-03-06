import React from "react";
import { Link } from "react-router-dom";
import NavBar from '../Nav/Nav'
import GardenUserPrint from '../Garden/GardenUserPrint'
import { UserReportList } from "../Reports/UserReport";
import './User.css';






const User = ({ user }) => {
  
    
    return (
      <>
      
        <NavBar />
        <br></br>
        <div className="full-width"><button className="garden-button"><a className="user-garden-button" href="#garden">View garden</a></button></div>
        
        <div className="main-user-container">
         
          <div className="user-row">
            <article>
              <section className="welcome-section">
          
               
               <h1 className="header-landing-text">Current Courses</h1> 
               <p>Click the course you'd like to view.</p>
               </section>
              <section className="card-container">
                {user.courses?.map((c) => {
               return (

                 <article className="course-card">
                 <Link className="class-card-text" to={`/course/${c.id}`}>{c.name}</Link>
                </article>
                )
                })}
                </section>
                
            </article>

            <article id="garden" className ="user-garden-section">
               <h3 className="header-landing-text">Instructor Garden</h3>
              <p>These are the flowers you've helped to grow--<br/>all of the flowers from your students' gardens! </p>
                <section className="print-section">
                  <GardenUserPrint />
                </section>
            </article>

          </div> 

           <div className="report-list-container">
           <UserReportList/>
           </div>
         
          </div>
        
     </>
    );
  };
  
  export default User;