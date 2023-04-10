import Home from "./componants/page/auth/Home";
import HomeAdmin from "./componants/page/admin/Home";
import HomeUser from "./componants/page/user/Home";
import Login from "./componants/page/auth/Login";
import Resgiter from "./componants/page/auth/Resgiter";
import { Button } from 'react-bootstrap';
import { Routes, Route } from "react-router-dom";
import  Navbar  from "./componants/layout/Navbar";


function App() {
  return (
    <div className="App">
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Resgiter />} />
        <Route path="/admin/index" element={<HomeAdmin />} />
        <Route path="/user/index" element={<HomeUser />} />
      </Routes>
    </div>
  );
}

export default App;
