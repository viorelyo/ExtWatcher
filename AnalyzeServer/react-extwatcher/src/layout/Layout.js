import React from 'react';
import TopMenu from "../layout/TopMenu";
import SideMenu from "../layout/SideMenu";


export default props => (
    <div className="grid">
        <div className="menu">
            <TopMenu/>
        </div>
        <div className="main-content">
            <SideMenu>
                {/* {console.log(props)} */}
                {props.children}
            </SideMenu>
        </div>
    </div>
);