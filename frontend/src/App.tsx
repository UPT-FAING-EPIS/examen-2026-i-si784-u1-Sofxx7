import { useEffect, useState } from 'react'
import axios from 'axios'
import './index.css'

interface Tournament {
  id: string
  name: string
  sport: string
  status: string
}

function App() {
  const [tournaments, setTournaments] = useState<Tournament[]>([])
  
  // Auth state
  const [token, setToken] = useState<string | null>(localStorage.getItem('token'))
  const [role, setRole] = useState<string | null>(localStorage.getItem('role'))
  const [userId, setUserId] = useState<string | null>(localStorage.getItem('userId'))
  const [isLogin, setIsLogin] = useState(true)
  
  // Form state
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [username, setUsername] = useState('')
  const [formRole, setFormRole] = useState('Player')

  // Creation State
  const [newTournamentName, setNewTournamentName] = useState('')
  const [newTournamentSport, setNewTournamentSport] = useState('Fútbol')

  useEffect(() => {
    if (token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
      fetchTournaments()
    }
  }, [token])

  const fetchTournaments = () => {
    axios.get('http://127.0.0.1:5179/api/Tournaments')
      .then(res => setTournaments(res.data))
      .catch(err => console.error("Error cargando torneos", err))
  }

  const handleCreateTournament = async (e: React.FormEvent) => {
    e.preventDefault()
    try {
      await axios.post('http://127.0.0.1:5179/api/Tournaments', {
        name: newTournamentName,
        description: "Torneo creado desde la web",
        sport: newTournamentSport,
        startDate: new Date().toISOString(),
        endDate: new Date(new Date().getTime() + 7 * 24 * 60 * 60 * 1000).toISOString(),
        maxTeams: 16,
        organizerId: userId
      })
      alert("Torneo creado exitosamente")
      setNewTournamentName('')
      fetchTournaments()
    } catch (error) {
      alert("Error al crear torneo. Asegúrate de tener permisos.")
    }
  }

  const handleJoinTournament = async (tournamentId: string) => {
    const teamName = window.prompt("Ingresa el nombre de tu equipo para inscribirte:")
    if (!teamName) return

    try {
      // Inscribimos el equipo. El backend toma el Capitan desde el JWT.
      await axios.post('http://127.0.0.1:5179/api/Teams', {
        name: teamName,
        tournamentId: tournamentId,
        captainId: userId
      })
      alert("¡Equipo inscrito con éxito en el torneo!")
    } catch (error) {
      alert("Error al inscribir el equipo.")
    }
  }

  const handleAuth = async (e: React.FormEvent) => {
    e.preventDefault()
    try {
      const url = isLogin ? 'http://127.0.0.1:5179/api/Auth/login' : 'http://127.0.0.1:5179/api/Auth/register'
      const payload = isLogin ? { email, password } : { username, email, password, role: formRole }
      
      const res = await axios.post(url, payload)
      const data = res.data
      
      localStorage.setItem('token', data.token)
      localStorage.setItem('role', data.role)
      localStorage.setItem('userId', data.userId)
      setToken(data.token)
      setRole(data.role)
      setUserId(data.userId)
    } catch (error: any) {
      console.error(error.response?.data || error.message);
      alert("Error en la autenticación: " + (error.response?.data?.title || error.message))
    }
  }

  const handleLogout = () => {
    localStorage.removeItem('token')
    localStorage.removeItem('role')
    localStorage.removeItem('userId')
    setToken(null)
    setRole(null)
    setUserId(null)
  }

  if (!token) {
    return (
      <div className="auth-container">
        <div className="auth-card">
          <h1>🏆 Tournament App</h1>
          <h2>{isLogin ? 'Iniciar Sesión' : 'Crear Cuenta'}</h2>
          <form onSubmit={handleAuth}>
            {!isLogin && (
              <>
                <input type="text" placeholder="Usuario" required value={username} onChange={e => setUsername(e.target.value)} />
                <select value={formRole} onChange={e => setFormRole(e.target.value)}>
                  <option value="Player">Soy Jugador</option>
                  <option value="Organizer">Soy Organizador (Crear Torneos)</option>
                </select>
              </>
            )}
            <input type="email" placeholder="Correo" required value={email} onChange={e => setEmail(e.target.value)} />
            <input type="password" placeholder="Contraseña" required value={password} onChange={e => setPassword(e.target.value)} />
            
            <button type="submit" className="btn-primary">{isLogin ? 'Entrar' : 'Registrarse'}</button>
          </form>
          <p onClick={() => setIsLogin(!isLogin)} className="auth-switch">
            {isLogin ? '¿No tienes cuenta? Regístrate' : '¿Ya tienes cuenta? Inicia sesión'}
          </p>
        </div>
      </div>
    )
  }

  return (
    <div className="container">
      <header>
        <h1>🏆 Torneos Deportivos</h1>
        <p>Plataforma oficial. Tu rol actual: <strong>{role}</strong></p>
        <button className="btn-logout" onClick={handleLogout}>Cerrar Sesión</button>
      </header>
      
      <main>
        {(role === 'Organizer' || role === 'Admin') ? (
          <div className="organizer-panel">
            <h2>Panel de Organizador / Admin</h2>
            <form onSubmit={handleCreateTournament} style={{ display: 'flex', flexDirection: 'row', gap: '10px', alignItems: 'center', marginTop: '0' }}>
              <input type="text" placeholder="Nombre del Torneo" required value={newTournamentName} onChange={e => setNewTournamentName(e.target.value)} />
              <select value={newTournamentSport} onChange={e => setNewTournamentSport(e.target.value)}>
                <option>Fútbol</option>
                <option>Baloncesto</option>
                <option>Tenis</option>
              </select>
              <button type="submit" className="btn-create">+ Crear Torneo</button>
            </form>
            <p>Aquí puedes gestionar los torneos en tu control.</p>
          </div>
        ) : (
          <h2>Torneos Abiertos para Inscripción</h2>
        )}

        {tournaments.length === 0 ? (
          <p className="empty-state">No hay torneos registrados todavía.</p>
        ) : (
          <div className="grid">
            {tournaments.map(t => (
              <div key={t.id} className="card">
                <h3>{t.name}</h3>
                <p><strong>Deporte:</strong> {t.sport}</p>
                <span className="badge">{t.status}</span>
                {role === 'Player' && <button className="btn-join" onClick={() => handleJoinTournament(t.id)}>Inscribir a mi equipo</button>}
                {(role === 'Organizer' || role === 'Admin') && <button className="btn-manage" onClick={() => alert("Función de gestión de fixture en construcción.")}>Gestionar Partidos</button>}
              </div>
            ))}
          </div>
        )}
      </main>
    </div>
  )
}

export default App
