import React, { Component } from 'react';
import { connect } from 'react-redux';
import './SearchPage.css'
import { FaSearch, FaAngleLeft, FaRegTimesCircle } from "react-icons/fa";
import { Link } from 'react-router-dom';
import en_to_rus from '../_helpers/rus_to_en';

class SearchPage extends Component{
    state = {
        inputValue:''
    }

    handleChange =  (e) => {
        const { value } = e.target;
        this.setState(({inputValue}) => {
            const newValue = value;
            
            return{
                inputValue: newValue
            }
        })     
    }

        
    handleEmpaty = (e) => {
        e.preventDefault();
        this.setState((inputValue) => {
            const newValue = '';
            return{
                inputValue: newValue
            }
        })
    }



    onhabdelSubmit = (e) => {
        e.preventDefault()
    }


    render(){           
        const { inputValue} = this.state;          
        return(
            <div className="search_main">
            <form className="form_style" onSubmit={this.onhabdelSubmit}>
                <div className="form_main">
                    <div className="header_form">
                        <Link className="p_style p_style_size" to='/home'><FaAngleLeft /></Link>
                        <p className="p_style p_style_size">Поиск задачи</p>
                    </div>
                    <div>
                        <p onClick={this.handleSearch} className="btn_style p_style_size"><Link to={`/?search=${en_to_rus(inputValue)}`} >Искать</Link></p>
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


const connectedSearchPage = connect()(SearchPage);
export { connectedSearchPage as SearchPage };