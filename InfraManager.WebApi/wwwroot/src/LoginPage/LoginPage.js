import React from 'react';
import { connect } from 'react-redux';
import './loginPage.css'
import { userActions } from '../_actions';

class LoginPage extends React.Component {
    constructor(props) {
        super(props);

        // reset login status
        this.props.logout();

        this.state = {
            username: '',
            password: '',
            submitted: false
        };
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    handleSubmit = (e) => {
        e.preventDefault();

        this.setState({ submitted: true });
        const { username, password } = this.state;
        if (username && password) {
            this.props.login(username, password);
        }
    }

    render() {        
        const { username, password, submitted } = this.state;
        
        return (
            <div className="main">
                <div className="logo">
                    <h5 className='mt-3 f-size'>Service Desk</h5>
                    <h4 className="f-size">ИнфраМенеджер</h4>
                </div>
                <div className="login">
                <form className="mt-5" name="form" onSubmit={this.handleSubmit}>
                <div className={'form-group' + (submitted && !username ? ' has-error' : '')}>
                        <input type="text" placeholder="Url Сервераин" className="form-control form_input f-size" name="url" />
                    </div>
                    <div className={'form-group' + (submitted && !username ? ' has-error' : '')}>
                        <input type="text" placeholder="Логин" className="form-control form_input f-size" name="username" value={username} onChange={this.handleChange} />
                        {submitted && !username &&
                            <div className="help-block f-size">Username is required</div>
                        }
                    </div>
                    <div className={'form-group' + (submitted && !password ? ' has-error' : '')}>
                        <input type="password" placeholder="Парол" className="form-control form_input f-size" name="password" value={password} onChange={this.handleChange} />
                        {submitted && !password &&
                            <div className="help-block f-size">Password is required</div>
                        }
                    </div>
                    <div className="form-group">
                        <button className="buttonClass">ВОЙТИ</button>
                    </div>
                </form>
                </div>
            </div>
        );
    }
}

function mapState(state) {
    const { loggingIn } = state.authentication;
    return { loggingIn };
}

const actionCreators = {
    login: userActions.login,
    logout: userActions.logout
};

const connectedLoginPage = connect(mapState, actionCreators)(LoginPage);
export { connectedLoginPage as LoginPage };