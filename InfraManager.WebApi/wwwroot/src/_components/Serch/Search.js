import React, { Component } from 'react';
import './Search.css';

import { FaSearch, FaAngleLeft, FaRegTimesCircle } from "react-icons/fa";


export default class Search extends Component{
    state = {
        inputValue:'',
        userList:[]
    }

    handleChange = (e) => {
        let addList = this.props.usersList;
        addList = addList.filter((item) => {
            return item.client == e.target.value
        });
          this.setState({userList: addList});
    }

    handleEmpaty = (e) => {
        e.preventDefault();
        this.setState({
            inputValue:''
        })
    }

    addList = () => {
        this.setState({
            userList:this.props.usersList
        })
    }

    handleSubmit = (e) => {
        e.preventDefault();
    }
    render(){
        const { inputValue } = this.state;
        console.log(this.state.userList);
        
        return(
            <div className="search_main">
                <form className="form_style" onSubmit={this.handleSubmit}>
                    <div className="form_main">
                        <div className="header_form">
                            <p><FaAngleLeft /></p> <p className="p_style">Поиск задачи</p>
                        </div>
                        <div>
                            <button className="btn_style" type="submit">Искать</button>
                        </div>
                    </div>
                    <div className="input_style">
                        <p className="input_style-P"><FaSearch /></p>
                         <input className="input" value={inputValue} type="text" onChange={this.handleChange} /> 
                         <button className="btn_style_cansle" type="button" onClick={this.handleEmpaty}><FaRegTimesCircle /></button>
                    </div>
                </form>
            </div>
        )
    }
}