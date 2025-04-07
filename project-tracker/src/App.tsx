import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from "./pages/Home";
import { Login } from "./pages/Login";
import { Register } from "./pages/Register";
import { Projects } from "./pages/Projects";
import { ForgetUserName } from "./pages/ForgetUserName";

import { ResetPasswordRequest } from "./pages/ResetPasswordRequest";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/home" element={<Home />} />
        <Route path="/projects" element={<Projects />} />
        <Route path="/forgotUsername" element={<ForgetUserName />} />
        <Route path="/resetPass" element={<ResetPasswordRequest />} />
        /forgotUsername
      </Routes>
    </Router>
  );
}

export default App;
