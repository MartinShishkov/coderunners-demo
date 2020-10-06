import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import HomePage from './components/pages/Home';
import EditPage from './components/pages/Edit';
import ListPage from './components/pages/List';
import { NotFoundPage } from './components/pages/NotFound';
import { Navbar } from './components/Navbar';
import { ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

function App() {
    return (
        <Router>
            <main>
                <Navbar />
                <div className="container-fluid pt-4">
                    <Switch>
                        <Route path="/" exact component={HomePage} />
                        <Route path="/settlements" exact component={ListPage} />
                        <Route path="/edit/:id" component={EditPage} />
                        <Route component={NotFoundPage} />
                    </Switch>
                </div>
                <ToastContainer autoClose={3000} position={"bottom-right"} hideProgressBar />
            </main>
        </Router>
    );
}

export default App;