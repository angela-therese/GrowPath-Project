import React from "react";
import { Routes, Route } from "react-router-dom";
import Login from "./components/Auth/Login";
import UserDetails from "./components/User/UserDetails";
import StudentDetails from "./components/Student/StudentDetails";
import CourseDetails from "./components/Course/CourseDetails";
import GoalDetails from "./components/Goal/GoalDetails";
import GoalForm from "./components/Goal/GoalForm";
import GoalEditForm from "./components/Goal/GoalEditForm";
import FlowerForm from "./components/Garden/FlowerForm";
import FlowerEditForm from "./components/Garden/FlowerFormEdit";
import { FlowerDelete } from "./components/Garden/FlowerDelete";
import { GoalDelete } from "./components/Goal/GoalDelete";



const ApplicationViews = () => {

  
    return (
      <Routes>
          <Route path="/" element={<Login/>}>
          </Route>

          <Route exact path="/user/:id" element={<UserDetails/>}>
          </Route>

          <Route exact path="/course/:id" element={<CourseDetails/>}>
          </Route>

          <Route exact path="/student/:id" element={<StudentDetails/>}>
          </Route>

          <Route exact path="/student/:studentId/goal/:goalId" element={<GoalDetails/>}>
          </Route>

          <Route exact path="/goal/edit/:goalId" element={<GoalEditForm/>}>
          </Route>

          <Route exact path="/student/:studentId/goal/add" element={<GoalForm/>}>
          </Route>

          <Route exact path="/student/:studentId/goal/:goalId/flower/add" element={<FlowerForm/>}>
          </Route>

          <Route exact path="/student/:studentId/flower/edit/:flowerId" element={<FlowerEditForm/>}>
          </Route>

          <Route exact path="/student/:studentId/flower/delete/:flowerId" element={<FlowerDelete/>}>
          </Route>

          <Route exact path="student/:studentId/goal/:goalId/delete" element={<GoalDelete/>}>
          </Route>

          


        </Routes>
        )
    }

    export default ApplicationViews