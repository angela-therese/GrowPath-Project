import React from "react";
import { Link } from "react-router-dom";
import NavBar from '../Nav/Nav'
import GardenUserPrint from '../Garden/GardenUserPrint'
import './User.css';





const User = ({ user }) => {
  
    // const { logout } = useContext(AuthContext);
    // const { id } = useParams();
    
    
    return (
      <>
      
        <NavBar />
        <br></br>
        <div className="main-user-container">
          <section className="card-container" >
             <section className="welcome-section">
              <h1 className="header-landing-text">Current Courses</h1> 
            </section>
       
           <h3 className="bold-text">Click the course you'd like to view.</h3>
             {user.courses?.map((c) => {
              return (
            // 
            <>
               <article className="course-card">
               <Link className="class-card-text" to={`/course/${c.id}`}>{c.name}</Link>
              </article>
            </>
          )
        })}
         </section>

        <section className ="user-garden-section">
            <h2>Instructor Garden</h2>
            <p>These are the flowers you've helped to grow--<br/>all of the flowers from your students' gardens! </p>
              <section className="print-section">
                <GardenUserPrint />
              </section>
        </section>
        </div>
       
     </>
    );
  };
  
  export default User;