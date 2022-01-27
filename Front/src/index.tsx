import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react'
import ReactDOM from 'react-dom'
import App from './App/App'

import { configure } from "mobx"

configure({
    enforceActions: "never",
})

ReactDOM.render(<App />, document.getElementById('root'))
