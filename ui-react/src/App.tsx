import { FC, useEffect } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import AppRouter from './components/AppRouter';
import { useActions } from './hooks/useActions';
import { AuthActionCreators } from './store/reduce/auth/action-creators';
import { useTypedSelector } from './hooks/useTypedSelector';
import LoadingPage from './pages/LoadingPage';

const App: FC = () => {
  const { isAuthLoading } = useTypedSelector(state => state.auth)
  const { auth } = useActions(AuthActionCreators)
  useEffect(() => {
    auth()
  }, [])
  return (
    !isAuthLoading
      ?
      <AppRouter />
      :
      <LoadingPage />
  )
}

export default App;
