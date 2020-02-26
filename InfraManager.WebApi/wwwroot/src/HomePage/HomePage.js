import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import './HomePage.css'

import { userActions } from '../_actions';
import { FaSearch, FaAngleLeft, FaRegTimesCircle,FaAngleDown } from "react-icons/fa";
import { IoIosLogOut } from "react-icons/io";

import { HamburgerButton } from 'react-hamburger-button';
//helpers
import handleMonth from '../_helpers/getMonth';
import getColorStatus from '../_helpers/getColor';

class HomePage extends Component {
    state = {
        open: false,
        isSerched:true,
        items:10,
        users:[],
        searchReasult:[]
    };

    componentDidMount() {
        const { items } = this.state
        this.getUsersList(this.props.location.search.substring(8),items);
        this.scrol();
    }


    getUsersList  = (key,limit) => {
        let user = JSON.parse(localStorage.getItem('user'));
        const requestOptions = {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization':'Bearer ' + user.token
         }
        };
        let initialUsers = [];
        fetch(`http://localhost:5000/api/Call/GetList?limit=${limit}&search=${key}`, requestOptions)
            .then(response => {        
                return response.json();
            }).then(data => {
                initialUsers = data.map((users) => {
                return users
            });
            if(key !== ''){
                this.setState(({users}) => {
                    const newUsers = initialUsers;
    
                    return{
                        searchReasult:newUsers
                    }
                });
            }
            this.setState(({users}) => {
                const newUsers = initialUsers;

                return{
                    users:newUsers
                }
            });
        });
    }
      

    scrol =  () => {    
        let lastElem = window.document.getElementById('bottomDiv');
        let react = this;
        window.addEventListener('scroll', function(e) {
            if ( this.innerHeight + this.scrollY === lastElem.offsetTop ) {
                react.setState(({items}) => {
                    const newItesm = items + 10;
                    return{
                        items:newItesm
                    }
                });                
                const { items } = react.state;
                react.getUsersList('', items)
            }        
        });
    }


    handleClick = () => {
        this.setState({
            open: !this.state.open,
            isSerched:!this.state.isSerched
        });
    }

    handleLogout = (e) => {
        console.log(e); 
    }

    handleDeleteUser(id) {
        return (e) => this.props.deleteUser(id);
    }

    handleClear = () => {
        this.setState(({query}) => {
            const newQuery = false;
            return {
                query:newQuery
            }
        })
    }

    render() {
        const { open, users, searchReasult } = this.state;
        const display = open ?
         <div className="drop_down" style={{display:"block"}}><Link style={{color:"#fff"}}  to="/login"><IoIosLogOut /></Link></div>:
         <div className="drop_down" style={{display:"none"}}><Link to="/login"><IoIosLogOut /></Link></div>
        const data = searchReasult.length ? searchReasult: users;
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
         <h4 onClick={this.onToogle}><Link to="/search" style={{color:'#fff'}}><FaSearch /></Link></h4>
         </div>
         <div className='serch_resault'>
         {this.props.location.search.substring(8)?
            <div className="serched_content">
                <Link to="/search"><FaAngleLeft /></Link>
                <p>Поиск: {this.props.location.search.substring(8)}</p>
                <Link onClick={this.handleClear} to="/home"><FaRegTimesCircle /></Link>
            </div> :
            <p style={{fontSize:'40px'}}>Показаны: ВСЕ <sub><FaAngleDown /></sub></p>
         }
         </div>
         {users.loading && <em>Loading users...</em>}
         <div className='error'>
         {users.error && <span className="text-danger">ERROR: {users.error = 'результат не ненавид'}</span>}
         </div>
         {
             <div className="parent">
                 {data.map((users, index) =>
                     <div className="child" key={index}>
                         <img src={`data:image/png;base64,${users.icon}`} alt={'PHONE LOGO'} style={{width:'120px',height:'120px'}}/>
                         <div className="content">
                             <div className="time">     
                                 <div className='color_main'>
                                     <div className="color_status" style={{background:`${getColorStatus(users.priorityColor)}`}}>
                                </div>{users.number}
                            </div>
                                 <p>{handleMonth(users.dateRegist)}</p>
                             </div>
                             <div className="items">
                                 <h5><strong>{users.summaryName}</strong></h5>
                                 <p>{users.client}</p>
                                 <p>Состаиани: {users.state}</p>
                             </div>
                         </div>
                     </div>
                 )}
             </div>
         }
            <div id="bottomDiv"></div>
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