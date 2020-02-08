import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { Segment, Header, Grid } from "semantic-ui-react";
import MainLoader from "../utils/Loader";
import { Files } from "../components/Files";


const Home = props => {
    const [files, setFiles] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetch("/api/analyzed-files",
            {
                headers : { 
                'Content-Type': 'application/json',
                'Accept': 'application/json'
                }
        
            }
        ).then(response =>
            response.json().then(data => {
                console.log(data);
                setFiles(data.files);
                setLoading(false);
            })
        );
    }, []);
    
    
    return (
        <Segment>
            <Header>Files</Header>
                {loading && <MainLoader />}
                <Files files={files} />
        </Segment>
    );
}

export default connect()(Home);