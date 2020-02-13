import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import './HomePage.css'

import { userActions } from '../_actions';

import phoneLogo from '../images/phone.png';
import { FaSearch } from "react-icons/fa";
import { IoIosLogOut } from "react-icons/io";


import { HamburgerButton } from 'react-hamburger-button';


import Serch from '../_components/Serch';





class HomePage extends React.Component {

    state = {
        open: false,
      };
    componentDidMount() {
        this.props.getUsers();
    }

    handleMonth = (date) => {
        let d = new Date(date);
        let month = new Array();
        month[0] = "January";
        month[1] = "February";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "November";
        month[11] = "December";
        let n = month[d.getMonth()];
        let day = d.getDate();
        return day + ' ' + n
    }

    handleClick = () => {
        this.setState({
            open: !this.state.open
        });
    }

    handleLogout = (e) => {
        console.log(e); 
    }

    handleDeleteUser(id) {
        return (e) => this.props.deleteUser(id);
    }

    render() {
        const { user, users } = this.props;
        const { open } = this.state;        
        const display = open ?
         <div className="drop_down" style={{display:"block"}}><Link style={{color:"#fff"}}  to="/login"><IoIosLogOut /></Link></div>:
         <div className="drop_down" style={{display:"none"}}><Link to="/login"><IoIosLogOut /></Link></div>
        return (
            <div className="mainUsers">
                <div className="header">
                <div className="toggle">
                    <HamburgerButton
                        open={this.state.open}
                        onClick={this.handleClick}
                        width={50}
                        height={50}
                        strokeWidth={1}
                        color='#fff'
                        animationDuration={0.5}
                    />
                    {display}
                </div>
                <h4 style={{color:'#fff', fontSize:'50px'}}>Задачи</h4>
                <h4><Link to="#" style={{color:'#fff'}}><FaSearch /></Link></h4>
                </div>
                {users.loading && <em>Loading users...</em>}
                {users.error && <span className="text-danger">ERROR: {users.error}</span>}
                {users.items &&
                    <div className="parent">
                        {users.items.map((users, index) =>
                            <div className="child" key={index}>
                                <img src={phoneLogo} alt="phone"/>
                                <div className="content">
                                    <div className="time">
                                        <p>49079</p>
                                        <p>{this.handleMonth(users.dateRegist)}</p>
                                    </div>
                                    <div className="items">
                                        <h5><strong>{users.summaryName}</strong></h5>
                                        <p>Client: {users.client}</p>
                                        <p>Состаиани: {users.state}</p>
                                    </div>
                                </div>
                            </div>
                        )}
                        <Serch usersList={users.items} />
                    </div>
                }
            </div>
        );
    }
}

function mapState(state) {
    const { users, authentication } = state;
    const { user } = authentication;
    return { user, users };
}

const actionCreators = {
    getUsers: userActions.getAll,
    deleteUser: userActions.delete
}

const connectedHomePage = connect(mapState, actionCreators)(HomePage);
export { connectedHomePage as HomePage };