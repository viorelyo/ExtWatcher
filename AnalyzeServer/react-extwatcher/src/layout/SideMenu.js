import React, { Component } from 'react';
import { Menu } from 'semantic-ui-react'
import { Link } from "react-router-dom";
import TextIcon from "../utils/TextIcon";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../store/SideMenu";


class SideMenu extends Component {
    state = {
        activeItem: 'dashboard',
    };

    handleItemClick = (e, {name}) => this.setState({activeItem: name});
    changeSize = () => this.setState({smallSidebar: !this.props.smallMenu});

    getMenu() {
        const {activeItem} = this.state;
        return (
            <Menu fixed='left' borderless className={(this.props.smallMenu ? 'small-side' : '') + ' side'} vertical>
                <Menu.Item as={Link} to={'/'} name='dashboard' active={activeItem === 'dashboard'}
                            onClick={this.handleItemClick}>
                    <TextIcon hideText={this.props.smallMenu} color='teal' name='home'>
                        Dashboard
                    </TextIcon>
                </Menu.Item>
            </Menu>
        );
    }

    render() {
        return (
            <div className='parent'>
                <div className={(this.props.smallMenu ? 'small-side ' : '') + 'side'}>
                    {this.getMenu()}
                </div>
                <div className={(this.props.smallMenu ? 'small-content ' : '') + 'content'}>
                    {this.props.children}
                </div>
            </div>
        );
    }
}

export default connect(
    state => state.sideMenu,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(SideMenu);