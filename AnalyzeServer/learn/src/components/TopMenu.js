import React, { useState } from 'react';
import { Dropdown, Icon, Menu, Segment } from 'semantic-ui-react'

function TopMenu() {
    return (
        <div>
            <Menu attached='top' className="top-menu">
            <Menu.Menu position='left'>
                <Menu.Item
                    name='home'>
                    <Icon name='home' color='teal' />
                    Home
                </Menu.Item>
            </Menu.Menu>

            <Menu.Menu position='right'>
                <div className='ui right aligned category search item'>
                <div className='ui transparent icon input'>
                    <input
                    className='prompt'
                    type='text'
                    placeholder='Search...'
                    />
                    <i className='search link icon' />
                </div>
                <div className='results' />
                </div>
            </Menu.Menu>
            </Menu>
        </div>
    );
}

export default TopMenu;