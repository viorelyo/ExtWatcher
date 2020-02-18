import React, { useState } from 'react';
import { Icon, Menu } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

function SideMenu() {
    const [activeItem, setActiveItem] = useState("");

    const handleItemClick = (e, { name }) => setActiveItem(name);

    return (
        <div className='parent'>
        <Menu compact inverted icon='labeled' vertical fixed='left' borderless className="side">
        <Menu.Item
            name='home'
            active={activeItem === 'home'}
            onClick={handleItemClick}>
            <Icon name='home' color='teal' />
            Home
        </Menu.Item>

        <Menu.Item
            name='video camera'
            active={activeItem === 'video camera'}
            onClick={handleItemClick}>
            <Icon name='video camera' />
            Channels
        </Menu.Item>

        <Menu.Item
            name='video play'
            active={activeItem === 'video play'}
            onClick={handleItemClick}>
            <Icon name='video play' />
            Videos
        </Menu.Item>
        </Menu>
        </div>
    );
}

export default SideMenu;