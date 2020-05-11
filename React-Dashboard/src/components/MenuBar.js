import React, { useState } from 'react';
import { Icon, Menu } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

function MenuBar() {
    const [activeItem, setActiveItem] = useState("");

    const handleItemClick = (e, { name }) => setActiveItem(name);

    return (
        <Menu compact icon='labeled' vertical>
        <Menu.Item
            name='gamepad'
            active={activeItem === 'gamepad'}
            onClick={handleItemClick}
        >
            <Icon name='gamepad' />
            Games
        </Menu.Item>

        <Menu.Item
            name='video camera'
            active={activeItem === 'video camera'}
            onClick={handleItemClick}
        >
            <Icon name='video camera' />
            Channels
        </Menu.Item>

        <Menu.Item
            name='video play'
            active={activeItem === 'video play'}
            onClick={handleItemClick}
        >
            <Icon name='video play' />
            Videos
        </Menu.Item>
        </Menu>
    );
}

export default MenuBar;