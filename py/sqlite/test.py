from sqlalchemy import create_engine
from sqlalchemy import Column, Integer, String, ForeignKey
from sqlalchemy.orm import relationship, sessionmaker
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()

def get_db(echo=False):
    engine = create_engine("sqlite:///database.db", echo=echo)
    with engine.connect() as connection:
        connection.execute("PRAGMA foreign_keys = ON;")
    return engine

def get_session(db):
    Session = sessionmaker(bind=db)
    session = Session()
    return session

def fetch_items(session, _class):
    print("==== Fetching ", _class.__tablename__)
    for item in session.query(_class):
        print(vars(item))
    print()

class Grandparent(Base):
    __tablename__ = "grandparents"
    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)

    parents = relationship("Parent", back_populates="grandparent")

class Parent(Base):
    __tablename__ = "parents"
    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)
    grandparent_id = Column(Integer, ForeignKey("grandparents.id"), nullable=False)

    grandparent = relationship("Grandparent", back_populates="parents")
    children = relationship("Child", back_populates="parent")

class Child(Base):
    __tablename__ = "children"
    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)
    parent_id = Column(Integer, ForeignKey("parents.id"), nullable=False)
    type_id = Column(Integer, ForeignKey("types.id"), nullable=False)

    parent = relationship("Parent", back_populates="children")
    type = relationship("Type", back_populates="children")

class Type(Base):
    __tablename__ = "types"
    id = Column(Integer, primary_key=True)
    name = Column(String, nullable=False)

    children = relationship("Child", back_populates="type")

def getType(session, id):
    return session.query(Type).filter(Type.id == id).first()

def main():
    db = get_db(True)
    session = get_session(db)
    querySession = get_session(db)

    for t_index in range(1, 3):
        t = Type()
        t.name = "type {}".format(t_index)
        session.add(t)
    session.commit()
    fetch_items(session, Type)
    first_type = session.query(Type).filter(Type.id == 1).first()
    print("first type:", vars(first_type))
    types = session.query(Type).all()

    try:
        for gp_index in range(1, 2):
            gp = Grandparent()
            gp.name = "grand {}".format(gp_index)
            for p_index in range(1, 3):
                p = Parent()
                p.name = "parent {}.{}".format(gp_index, p_index)
                p.grandparent = gp
                # session.add(p)
                for c_index in range(1, 5):
                    c = Child()
                    c.name = "child {}.{}.{}".format(gp_index, p_index, c_index)
                    c.parent = p
                    type = next((t for t in types if t.id == (c_index % 2) + 1), None)
                    print("Seleted type", vars(type))
                    
                    c.type = type
                    # session.add(c)

            session.add(gp)
    
        session.commit()

        
        fetch_items(session, Grandparent)
        fetch_items(session, Parent)
        fetch_items(session, Child)

    except Exception as ex:
        session.rollback()
        print("Error; ", vars(ex.args))        



if __name__ == "__main__":
    main()