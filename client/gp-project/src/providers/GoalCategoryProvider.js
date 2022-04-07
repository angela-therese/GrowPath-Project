import React  from "react"
// import { StudentContext } from "./StudentProvider";
import { createContext, useState } from "react";

export const GoalCategoryContext = createContext();

export const GoalCategoryProvider = (props) => {
    
    const baseUrl = 'https://localhost:44362';

    const [categories, setCategories] = useState([]);

    const getCategories = () => {
        return fetch (`${baseUrl}/api/goalcategory`)
        .then((res) => res.json())
        .then(setCategories)
    }




return (
    <GoalCategoryContext.Provider
      value={{ categories, getCategories}}
    >
      {props.children}
    </GoalCategoryContext.Provider>
  );
}