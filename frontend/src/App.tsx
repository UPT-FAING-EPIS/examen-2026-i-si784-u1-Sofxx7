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
  const [role, setRole] = useState<'Player' | 'Organizer'>('Player')

  useEffect(() => {
    axios.get('http://localhost:5179/api/Tournaments')
      .then(res => setTournaments(res.data))
      .catch(err => console.error("Error cargando torneos", err))
  }, [])

  return (
    <div className="container">
      <header>
        <h1>🏆 Torneos Deportivos</h1>
        <p>Plataforma oficial de gestión de torneos en línea.</p>
        
        <div className="role-toggle">
          <button 
            className={role === 'Player' ? 'active' : ''} 
            onClick={() => setRole('Player')}>Vista Jugador</button>
          <button 
            className={role === 'Organizer' ? 'active' : ''} 
            onClick={() => setRole('Organizer')}>Vista Organizador</button>
        </div>
      </header>
      
      <main>
        {role === 'Organizer' ? (
          <div className="organizer-panel">
            <h2>Panel de Organizador</h2>
            <button className="btn-create">+ Crear Nuevo Torneo</button>
            <p>Aquí puedes gestionar los torneos que has creado.</p>
          </div>
        ) : (
          <h2>Torneos Abiertos para Inscripción</h2>
        )}

        {tournaments.length === 0 ? (
          <p className="empty-state">No hay torneos registrados todavía. ¡Ve a Swagger para crear uno!</p>
        ) : (
          <div className="grid">
            {tournaments.map(t => (
              <div key={t.id} className="card">
                <h3>{t.name}</h3>
                <p><strong>Deporte:</strong> {t.sport}</p>
                <span className="badge">{t.status}</span>
                {role === 'Player' && <button className="btn-join">Inscribir a mi equipo</button>}
                {role === 'Organizer' && <button className="btn-manage">Gestionar</button>}
              </div>
            ))}
          </div>
        )}
      </main>
    </div>
  )
}

export default App
