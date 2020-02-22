import React from 'react';
import { Menu, Image, Input, Form, Icon } from 'semantic-ui-react'
import './TopMenu.scss'
import logo from '../../assets/images/logo.jpg'


function TopMenu() {
    return (
        <Menu inverted borderless fixed='top' className='top-menu'>
            <Menu.Item header className='logo'>
                <Image src={logo} size='tiny'/>
            </Menu.Item>
        
        <Menu.Menu className='nav-container'>
            <Menu.Item className='search-input'>
                <Form>
                    <Form.Field>
                        <Input placeholder='Search' size='small' action='Go'/>
                    </Form.Field>
                </Form>
            </Menu.Item>
        </Menu.Menu>

        <Menu.Menu position='right'>
            <Menu.Item name='about'>
                <Icon size='big' name='question circle outline'/>
            </Menu.Item>
        </Menu.Menu>
        </Menu>
    );
}

export default TopMenu;